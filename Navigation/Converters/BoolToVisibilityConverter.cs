﻿using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System.Diagnostics;

namespace Navigation.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Debug.WriteLine("BoolToVisibilityConverter.Convert called with value: " + value);
            if (value is bool flag)
            {
                return flag ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Debug.WriteLine("BoolToVisibilityConverter.ConvertBack called with value: " + value);
            if (value is Visibility vis)
            {
                return vis == Visibility.Visible;
            }
            return false;
        }
    }
}
