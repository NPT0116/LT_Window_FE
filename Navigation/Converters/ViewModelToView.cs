using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

using Navigation.Views.Inventory;
using Navigation.Views;

using PhoneSelling.ViewModel.Pages.Authentication;
using PhoneSelling.ViewModel.Pages.Inventory;
using PhoneSelling.ViewModel.Pages;



namespace Navigation.Converters
{
    internal class ViewModelToView : IValueConverter
    {
        private static readonly Dictionary<Type, Type> pairs = new Dictionary<Type, Type>()
        {
            // MainPage
            {typeof(MainPageViewModel),typeof(MainPage)},
            {typeof(LoginPageViewModel),typeof(LoginPage)},
            // Inventory
            {typeof(ItemDetailPageViewModel), typeof(ItemDetailPage)},
            {typeof(CreateItemsPageViewModel), typeof(CreateItemsPage)},
            {typeof(InventoryAdjustmentsPageViewModel), typeof(InventoryAdjustmentsPage) },
            {typeof(PhonePageViewModel),typeof(PhonePage)},
            // Dashboard
            {typeof(DashboardPageViewModel), typeof(DashboardPage)},
            // ReportPage
            {typeof(ReportPageViewModel), typeof(ReportPage)},
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
