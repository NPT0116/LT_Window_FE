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
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
using Navigation.Helpers;
using System.Globalization;

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

            //var flyout = new Flyout();
            //var calendarView = new CalendarView();
            //calendarView.SelectedDates.Add(DateTimeOffset.Now);

            //calendarView.SelectedDatesChanged += CalendarPopup_SelectedDatesChanged;
            //flyout.Content = calendarView;

            // Attach Flyout to the Button
            //FlyoutBase.SetAttachedFlyout(DatePickerButton, flyout);
            // Notify
            WeakReferenceMessenger.Default.Register<Message>(this, (r, m) =>
            {
                DialogHelper.ShowDialogAsync(m.status ? "THÀNH CÔNG" : "LỖI",m.message,"ĐÓNG", this.XamlRoot);
            });
        }
        private async void CreateCustomer_Click(object sender, RoutedEventArgs args)
        {
            await CreateCustomerDialog.ShowAsync();
        }
        private async void CustomerSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput) return;
            var text = sender.Text.ToLower().Trim();
            try
            {
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
                else
                {
                    Debug.WriteLine("Search by email");
                    customer = await ViewModel.SearchCustomersByEmail(text);
                }
                var customerSuggestions = customer != null ? new List<Customer>() { customer } : new List<Customer>();
                //customerSuggestions.Add(new Customer { Name = "➕ Create New Customer", Phone = "", Email = "" });

                sender.ItemsSource = customerSuggestions;
            }
            catch
            {

            }
            
        }

        private async void CustomerSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Customer selectedCustomer)
            {
                Debug.WriteLine("set sender text");
                sender.Text = selectedCustomer.Name; // Set input to customer's name
                ViewModel.Customer = selectedCustomer;
                ViewModel.Invoice.CustomerID = selectedCustomer.CustomerID;
            }
        }

        private async Task ShowCreateCustomerDialog()
        {
            await CreateCustomerDialog.ShowAsync();
        }

        private async void CustomerDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                bool hasErrors = ViewModel.Customer.ValidateAllProperties();
                Debug.WriteLine(hasErrors);
                if (hasErrors)
                {
                    args.Cancel = true;
                    return;
                }

                Debug.WriteLine(JsonSerializer.Serialize(ViewModel.Customer, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure JSON uses camelCase
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull // Ignore null values
                }));
                try
                {
                    await ViewModel.CreateCustomer();
                    ViewModel.Invoice.CustomerID = ViewModel.Customer.CustomerID;
                    App.CustomerDictionary[ViewModel.Customer.CustomerID] = ViewModel.Customer.Name;
                    DialogHelper.ShowDialogAsync("THÀNH CÔNG", "Tạo khách hàng mới thành công", "Đóng", this.XamlRoot);
                } catch(Exception ex)
                {
                    ViewModel.Customer.Name = String.Empty;
                    ViewModel.Customer.Phone = String.Empty;
                    ViewModel.Customer.Email = String.Empty;
                    DialogHelper.ShowDialogAsync("LỖI", $"{ex.Message}", "Đóng", this.XamlRoot);
                }

            }
        }
        private void DatePickerButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        //private void CalendarPopup_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        //{
        //    if (args.AddedDates.Count > 0)
        //    {
        //        DateTime selectedDate = args.AddedDates[0].DateTime;
        //        SelectedDateText.Text = selectedDate.ToString("dd MMMM yyyy", new CultureInfo("vi-VN"));
        //        ViewModel.Invoice.Date = selectedDate.ToUniversalTime();
        //    }
        //}

        private async void ItemSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var variants = await App.SearchVariants(sender.Text);
                if(variants != null)
                {
                    sender.ItemsSource = variants.OrderBy(v => v.Item.ItemName).ToList();
                }
            }
        }

        private void ItemSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var row = (InvoiceDetail)sender.DataContext;
            if (args.SelectedItem is Variant variant)
            {
                sender.Text = $"{variant.Item.ItemName} - {variant.Color.Name} - {variant.Storage}";

                // Update the row's properties
                row.Variant = variant;
                row.VariantId = variant.VariantID;
                row.Price = variant.SellingPrice; // Ensure Price is taken from the selected Variant
            }
        }

        private void ItemTable_DragItemsStarting(object sender, DragItemsStartingEventArgs e) { }

        private void ItemTable_DragOver(object sender, DragEventArgs e) { }

        private void ItemTable_Drop(object sender, DragEventArgs e) { }

        private void StackPanel_ContextCanceled(UIElement sender, RoutedEventArgs args)
        {

        }
    }
}
