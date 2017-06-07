using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MainClientWindow.Converters {
    enum RequestStatus {
        NotFinished,
        Failed,
        Successful
    }

    public class TextBlockExtension : DependencyObject {
        //setting this class to non static and inheriting the DependencyObject compiles but the code inside doesnt seem to be getting hit
        //but the code im borrowing (https://stackoverflow.com/questions/27734084/create-hyperlink-in-textblock-via-binding) from has the extension as static without any inheritance
        //but when running that it throws the exception object must derive from Dependancy object 

        // https://www.nuget.org/packages/HtmlAgilityPack/
        // HTML parser
        // nuget cmd: Install-Package HtmlAgilityPack -Version 1.4.9.5

        private static HtmlWeb web;
        private static HtmlDocument document;
        private static RequestStatus requestStatus;

        public static string GetFormattedText(DependencyObject obj) { return (string)obj.GetValue(FormattedTextProperty); }

        public static void SetFormattedText(DependencyObject obj, string value) { obj.SetValue(FormattedTextProperty, value); }

        public static readonly DependencyProperty FormattedTextProperty =
            DependencyProperty.RegisterAttached("FormattedText", typeof(string), typeof(TextBlockExtension),
            new PropertyMetadata(string.Empty, (sender, e) => {
                string text = e.NewValue as string;

                var rtBox = sender as RichTextBox;
                if (rtBox != null) {
                    FlowDocument flowDoc = rtBox.Document;
                    flowDoc.Blocks.Clear();

                    Paragraph message = new Paragraph();
                    Paragraph metadata = new Paragraph();

                    message.Inlines.Clear();
                    metadata.Inlines.Clear();

                    Regex regex = new Regex(@"(https?://[^\s]+)");
                    var str = regex.Split(text);

                    for (int i = 0; i < str.Length; i++)
                        if (i % 2 == 0) {
                            message.Inlines.Add(new Run { Text = str[i] });
                        }
                        else {
                            Uri outUri;

                            if (Uri.TryCreate(str[i], UriKind.Absolute, out outUri)
                               && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps)) {
                                //Do something with your validated Absolute URI...
                                Hyperlink link = new Hyperlink { NavigateUri = new Uri(str[i]) };
                                link.RequestNavigate += HyperLink_ClickHandler;
                                link.Inlines.Add(new Run { Text = str[i] });
                                message.Inlines.Add(link);
                            }

                            // Metadata stuff
                            web = new HtmlWeb();
                            requestStatus = RequestStatus.NotFinished;
                            Thread timer = new Thread(() => StartTimer(2));
                            Thread requestThread = new Thread(() => WebRequest(str[i]));

                            timer.Start();
                            requestThread.Start();

                            while (requestStatus == RequestStatus.NotFinished) {}

                            if (requestStatus == RequestStatus.Successful) {
                                if (document.DocumentNode.Element("html") != null && document.DocumentNode.Element("html").Element("head") != null
                                    && document.DocumentNode.Element("html").Element("head").Elements("meta") != null) {

                                    IEnumerable<HtmlNode> metaTags = document.DocumentNode.Element("html").Element("head").Elements("meta");

                                    // TODO: check if good request

                                    foreach (HtmlNode hn in metaTags) {
                                        string tagString = hn.OuterHtml;

                                        if (tagString.Contains("name=\"description\"")) {
                                            // Must be a description for the page, lets output it
                                            regex = new Regex("(content=)(\".*\")");
                                            string fullString = regex.Match(tagString).Groups[0].Value;
                                            string content = fullString.Replace("content=\"", "").TrimEnd('"');

                                            metadata.Inlines.Add(new Run { Text = content });
                                        }
                                    }
                                }
                            }
                        }
                    
                    Thickness zero = new Thickness(0);
                    FontFamily ff = new FontFamily("Arial");

                    message.Padding = zero;
                    message.Margin = zero;
                    message.FontFamily = ff;
                    message.FontSize = 14;
                    flowDoc.Blocks.Add(message);

                    if (requestStatus == RequestStatus.Successful) {
                        metadata.Padding = zero;
                        metadata.Margin = zero;
                        metadata.FontFamily = ff;
                        metadata.FontSize = 14;
                        metadata.BorderThickness = new Thickness(1);
                        metadata.BorderBrush = new SolidColorBrush(Colors.Black);
                        flowDoc.Blocks.Add(metadata);
                    }

                    rtBox.Document = flowDoc;
                }
            }));

        private static void WebRequest(string url) {
            try {
                document = web.Load(url);
                requestStatus = RequestStatus.Successful;
            } catch (System.Net.WebException) {
                requestStatus = RequestStatus.Failed;
            }

            // TODO: do actual adding of metadata here if succcessful and what not
        }

        private static void StartTimer(int seconds) {
            Thread.Sleep(seconds * 1000);
            if (requestStatus != RequestStatus.Successful) {
                requestStatus = RequestStatus.Failed;
            }
        }

        private static void HyperLink_ClickHandler(object sender, RoutedEventArgs e) {
            if (e.OriginalSource is Hyperlink) {
                Process.Start((e.OriginalSource as Hyperlink).NavigateUri.ToString());
                e.Handled = true;
            }

        }
    }
}
