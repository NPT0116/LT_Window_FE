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
using PhoneSelling.ViewModel.Pages.Payments.Invoices;
using PhoneSelling.Data.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Media.Audio;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Views.Payments.Invoices
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateInvoicePage : Page
    {
        public CreateInvoicePageViewModel ViewModel { get; set; }
        public CreateInvoicePage()
        {
            this.InitializeComponent();
            ViewModel = new();
            this.DataContext = ViewModel;

            var flyout = new Flyout();
            var calendarView = new CalendarView();
            calendarView.SelectedDatesChanged += CalendarPopup_SelectedDatesChanged;
            flyout.Content = calendarView;

            // Attach Flyout to the Button
            FlyoutBase.SetAttachedFlyout(DatePickerButton, flyout);
        }

        private async void CustomerSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput) return;
            var text = sender.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            var customer = new Customer();
            if (text[0] >= '0' && text[0] <= '9')
            {
                Debug.WriteLine("Search by phone");
                Debug.WriteLine(text);
                customer = await ViewModel.SearchCustomerByPhone(text);
            }
            //else
            //{
            //    Debug.WriteLine("Search by email");
            //    customers = await ViewModel.SearchCustomersByEmail(text);
            //}
            sender.ItemsSource = customer != null ? new List<Customer>() { customer } : new List<Customer>();
        }

        private void CustomerSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Customer selectedCustomer)
            {
                sender.Text = selectedCustomer.Name; // Set input to customer's name
                ViewModel.Customer = selectedCustomer; // Update ViewModel if needed
                ViewModel.Invoice.CustomerID = selectedCustomer.CustomerID;
            }
        }

        private void DatePickerButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void CalendarPopup_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            if (args.AddedDates.Count > 0)
            {
                DateTime selectedDate = args.AddedDates[0].DateTime;
                SelectedDateText.Text = selectedDate.ToString("dd MMM yyyy"); // Format like your image
                ViewModel.Invoice.Date = selectedDate.ToUniversalTime();
            }
        }

        private async void ItemSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var variants = await ViewModel.SearchVariants(sender.Text);
                if(variants != null)
                {
                    sender.ItemsSource = variants;
                }
            }
        }

        private void ItemSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var row = (InvoiceDetail)sender.DataContext;

            if (args.SelectedItem is Variant variant)
            {
                sender.Text = $"{variant.Item.ItemName} {variant.Storage} {variant.Color.Name}";

                // Update the row's properties
                row.Variant = variant;
                row.VariantId = variant.VariantID;
                row.Price = variant.SellingPrice; // Ensure Price is taken from the selected Variant

                
            }
        }

        private void ItemTable_DragItemsStarting(object sender, DragItemsStartingEventArgs e) { }

        private void ItemTable_DragOver(object sender, DragEventArgs e) { }

        private void ItemTable_Drop(object sender, DragEventArgs e) { }

    }
}
