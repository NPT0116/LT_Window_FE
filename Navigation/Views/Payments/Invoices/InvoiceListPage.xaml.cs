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
        private void CalendarFromDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate.HasValue)
            {
                var selectedDate = args.NewDate.Value.DateTime;
                ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.FromDate = DateTime.SpecifyKind(selectedDate, DateTimeKind.Utc);
            }
        }
        private void CalendarToDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate.HasValue)
            {
                var selectedDate = args.NewDate.Value.DateTime;
                ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.ToDate = DateTime.SpecifyKind(selectedDate, DateTimeKind.Utc);
            }
        }
        private void ResetDefault_button(object sender, RoutedEventArgs e)
        {
            CalendarFromDate.Date = null;
            CalendarToDate.Date = null;
            ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.FromDate = null;
            ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.ToDate = null;
            ViewModel.InvoiceQuery.Query.CustomerName = String.Empty;
            ViewModel.InvoiceQuery.Query.CustomerPhone = String.Empty;
            ViewModel.InvoiceQuery.LoadDataCommand.Execute(null);
        }

    }
}
