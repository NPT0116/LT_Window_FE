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
                var ci = new CultureInfo("vi-VN");
                ci.NumberFormat.NumberGroupSeparator = ",";
                ci.NumberFormat.NumberDecimalDigits = 0;
                return number.ToString("N0", ci);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
