using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceList;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Views.Payments.Invoices
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InvoiceListPage : Page
    {
        public InvoiceListViewModel ViewModel { get; set; }
        public InvoiceListPage()
        {
            this.InitializeComponent();
            ViewModel = new();
            this.DataContext = ViewModel;
        }

        private void CalendarFromDate_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            if (args.AddedDates.Count > 0)
            {
                ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.FromDate = args.AddedDates[0].DateTime;
                Debug.WriteLine($"From Date selected: {ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.FromDate}");
            }
        }

        private void CalendarToDate_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            if (args.AddedDates.Count > 0)
            {
                ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.ToDate = args.AddedDates[0].DateTime;
                Debug.WriteLine($"To Date selected: {ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.ToDate}");
            }
        }

    }
}
