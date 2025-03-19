using System;
using Microsoft.UI.Xaml.Data;
namespace Navigation.Converters
{
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                // ✅ Ensure the DateTime is valid before converting
                if (dateTime == DateTime.MinValue)
                {
                    return null; // Return null instead of an invalid DateTimeOffset
                }
                return new DateTimeOffset(dateTime);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.DateTime;
            }
            return null;
        }
    }

}
