using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace Navigation.Converters
{
    public class VietnamDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var culture = new CultureInfo("vi-VN");
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("dd/MM/yyyy", culture);
            } else if (value is DateTimeOffset dto)
            {
                return dto.ToString("dd/MM/yyyy", culture);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
