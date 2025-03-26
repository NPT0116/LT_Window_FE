using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation.Collections;

namespace Navigation.Converters
{
    public class TransactionTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string transactionType)
            {
                if (transactionType.Equals("Nhập", StringComparison.OrdinalIgnoreCase)) {
                    return new SolidColorBrush(Colors.Green);
                } else if (transactionType.Equals("Xuất", StringComparison.OrdinalIgnoreCase))
                {
                    return new SolidColorBrush(Colors.Red);
                }

            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
