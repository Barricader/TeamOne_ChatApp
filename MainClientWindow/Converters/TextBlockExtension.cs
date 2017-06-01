using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MainClientWindow.Converters
{
    public class TextBlockExtension : DependencyObject
    {
        //setting this class to non static and inheriting the DependencyObject compiles but the code inside doesnt seem to be getting hit
        //but the code im borrowing (https://stackoverflow.com/questions/27734084/create-hyperlink-in-textblock-via-binding) from has the extension as static without any inheritance
        //but when running that it throws the exception object must derive from Dependancy object excpetion
        public static string GetFormattedText(DependencyObject obj)
        { return (string)obj.GetValue(FormattedTextProperty); }

        public static void SetFormattedText(DependencyObject obj, string value)
        { obj.SetValue(FormattedTextProperty, value); }

        public static readonly DependencyProperty FormattedTextProperty =
            DependencyProperty.Register("FormattedText", typeof(string), typeof(TextBlockExtension),
            new PropertyMetadata(string.Empty, (sender, e) =>
            {
                string text = e.NewValue as string;
                Console.WriteLine(text);
                var textBl = sender as TextBlock;
                if (textBl != null)
                {
                    textBl.Inlines.Clear();
                    Regex regx = new Regex(@"(http|https)://[^\s]+", RegexOptions.IgnoreCase);
                    var str = regx.Split(text);
                    for (int i = 0; i < str.Length; i++)
                        if (i % 2 == 0)
                            textBl.Inlines.Add(new Run { Text = str[i] });
                        else
                        {
                            Hyperlink link = new Hyperlink { NavigateUri = new Uri(str[i]), Foreground = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush };
                            link.Inlines.Add(new Run { Text = str[i] });
                            textBl.Inlines.Add(link);
                        }
                }
            }));
    }
}
