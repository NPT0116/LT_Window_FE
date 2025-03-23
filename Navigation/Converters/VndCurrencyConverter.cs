using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace Navigation.Converters
{
    public class VndCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (double.TryParse(value.ToString(), out double number))
            {
                //return number.ToString("C0", new CultureInfo("vi-VN"));
                return string.Format(new CultureInfo("vi-VN"), "{0:N0}", number);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
