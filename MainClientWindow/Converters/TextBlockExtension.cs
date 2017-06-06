using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MainClientWindow.Converters {
    public class TextBlockExtension : DependencyObject {
        //setting this class to non static and inheriting the DependencyObject compiles but the code inside doesnt seem to be getting hit
        //but the code im borrowing (https://stackoverflow.com/questions/27734084/create-hyperlink-in-textblock-via-binding) from has the extension as static without any inheritance
        //but when running that it throws the exception object must derive from Dependancy object excpetion
        public static string GetFormattedText(DependencyObject obj) { return (string)obj.GetValue(FormattedTextProperty); }

        public static void SetFormattedText(DependencyObject obj, string value) { obj.SetValue(FormattedTextProperty, value); }

        public static readonly DependencyProperty FormattedTextProperty =
            DependencyProperty.RegisterAttached("FormattedText", typeof(string), typeof(TextBlockExtension),
            new PropertyMetadata(string.Empty, (sender, e) => {
                string text = e.NewValue as string;

                var rtBox = sender as RichTextBox;
                if (rtBox != null) {
                    FlowDocument flowDoc = rtBox.Document;
                    Paragraph message = new Paragraph();

                    Regex regx = new Regex(@"(https?://[^\s]+)");
                    var str = regx.Split(text);

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

                        }

                    flowDoc.Blocks.Add(message);
                    rtBox.Document = flowDoc;
                }
            }));

        private static void HyperLink_ClickHandler(object sender, RoutedEventArgs e) {
            if (e.OriginalSource is Hyperlink) {
                Process.Start((e.OriginalSource as Hyperlink).NavigateUri.ToString());
                e.Handled = true;
            }

        }
    }
}
