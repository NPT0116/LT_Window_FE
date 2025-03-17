using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System.Diagnostics;

namespace Navigation.Converters
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Debug.WriteLine("InverseBoolToVisibilityConverter.Convert called with value: " + value);
            if (value is bool flag)
            {
                return flag ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Debug.WriteLine("InverseBoolToVisibilityConverter.ConvertBack called with value: " + value);
            if (value is Visibility vis)
            {
                return vis != Visibility.Visible;
            }
            return false;
        }
    }
}
