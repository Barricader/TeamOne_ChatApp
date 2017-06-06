using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MainClientWindow.Converters {
    class NotBooltoVisConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = (bool) value;
            
            
            return (!flag ? Visibility.Visible : Visibility.Hidden);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}
