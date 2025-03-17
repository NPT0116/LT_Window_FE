using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Navigation.Views.Inventory;
using Navigation.Views;
using PhoneSelling.ViewModel.Pages.Inventory;
using PhoneSelling.ViewModel.Pages;
using PhoneSelling.ViewModel.Pages.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Converters
{
    internal class ViewModelToView : IValueConverter
    {
        private static readonly Dictionary<Type, Type> pairs = new Dictionary<Type, Type>()
        {
            {typeof(MainPageViewModel), typeof(MainPage) },

            {typeof(LoginPageViewModel),typeof(LoginPage)},
            {typeof(PhonePageViewModel),typeof(PhonePage)},
            // Inventory
            {typeof(VariantsDetailPageViewModel), typeof(VariantsDetailPage) },
            {typeof(DashboardPageViewModel), typeof(DashboardPage) }
        };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            pairs.TryGetValue(value.GetType(), out var page);
            Page x = (Page)Activator.CreateInstance(page);
            x.DataContext = value;
            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
