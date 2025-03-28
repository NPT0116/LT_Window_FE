using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;

namespace Navigation.Converters
{
    public class DateTimeNullableToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                return new DateTimeOffset(dateTime);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.DateTime;
            }
            return DependencyProperty.UnsetValue;
        }
    }

}
