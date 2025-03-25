using Navigation.Views;
using Navigation.Views.Inventory;
using Navigation.Views.Variants;

using Navigation.Views.Inventory.Variants;
using Navigation.Views.Inventory.ItemGroups;
using Navigation.Views.Inventory.CreateItems;

using PhoneSelling.ViewModel.Pages.Inventory.Variants;
using PhoneSelling.ViewModel.Pages.Inventory.ItemGroups;
using PhoneSelling.ViewModel.Pages.Inventory.CreateItemPages;


using PhoneSelling.ViewModel.Pages;
using PhoneSelling.ViewModel.Pages.Inventory;
using PhoneSelling.ViewModel.Pages.Variants;
using PhoneSelling.ViewModel.Pages.Authentication;

using PhoneSelling.ViewModel.Pages.Payments.Invoices;
using PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceList;
using PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceDetailPage;
using Navigation.Views.Payments.Invoices;

using PhoneSelling.ViewModel.Pages.Inventory.Transaction.InboundTransaction;
using Navigation.Views.Inventory.Transaction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System.Diagnostics;


namespace Navigation.Converters
{
    internal class ViewModelToView : IValueConverter
    {
        private static readonly Dictionary<Type, Type> pairs = new Dictionary<Type, Type>()
        {
            {typeof(MainPageViewModel), typeof(MainPage) },
            {typeof(DashboardPageViewModel), typeof(DashboardPage) },
            {typeof(LoginPageViewModel),typeof(LoginPage)},
            {typeof(PhonePageViewModel),typeof(PhonePage)},
            // Inventory
            {typeof(ItemGroupsPageViewModel), typeof(ItemGroupsPage)},
            {typeof(CreateItemPageViewModel), typeof(CreateItemPage)},
            {typeof(VariantsListPageViewModel), typeof(VariantsListPage)},
            {typeof(VariantListViewModel),typeof(VariantListPage)},
            {typeof(CreateInboundTransactionViewModel),typeof(CreateInboundTransactionPage)},

            // Invoice
            {typeof(InvoiceDetailPageViewModel), typeof(InvoiceDetailPage) },
            {typeof(CreateInvoicePageViewModel),typeof(CreateInvoicePage)},
            {typeof(InvoiceListViewModel),typeof(InvoiceListPage)},
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
