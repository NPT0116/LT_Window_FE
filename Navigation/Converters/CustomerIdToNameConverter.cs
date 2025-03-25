using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.DependencyInjection;


namespace Navigation.Converters
{
    public class CustomerIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Guid customerId)
            {
                if (App.CustomerDictionary.TryGetValue(customerId, out string customerName))
                {
                    return customerName;
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
