using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Windows.UI.Text;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceList;
using System.Diagnostics;
using PhoneSelling.ViewModel.Pages;


using Navigation.Views;
using PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceDetailPage;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Text;
using PhoneSelling.Data.Repositories.InvoiceRepository;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;

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
        private IVariantRepository _variantRepository { get; set; }

        public InvoiceListPage()
        {
            this.InitializeComponent();
            ViewModel = new InvoiceListViewModel();
            this.DataContext = ViewModel;
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();

        }


        private async void GoToInvoiceDetailPage(object sender, RoutedEventArgs args)
        {
            Debug.WriteLine("Pop up form ne");
            var button = sender as Button;
            var invoiceInfor = (Invoice)button.DataContext;
            var customerName = App.CustomerDictionary.TryGetValue(invoiceInfor.CustomerID, out var value) ? value: "";

            var content = new StackPanel
            {
                Children =
                {
                    new TextBlock
                    {
                        Text = invoiceInfor.InvoiceID.ToString(),
                        FontSize=25,
                        TextWrapping = TextWrapping.Wrap
                    },
                    new TextBlock
                    {
                        Text = invoiceInfor.Date.ToString(),
                        FontSize=25,
                        TextWrapping = TextWrapping.Wrap
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new TextBlock
                            {
                                Text="Tên khách hàng:",
                                FontWeight= FontWeights.Bold,
                            },
                            new TextBlock
                            {
                                Text=customerName,
                                FontSize=20,
                            }
                        },
                    },
                    new TextBlock
                    {
                        Text=invoiceInfor.TotalAmount.ToString(),
                    }

                },
            };
            foreach (var detail in invoiceInfor.InvoiceDetails)
            {
                var grid = new Grid
                {
                    Margin = new Thickness(0, 5, 0, 5)
                };

                // Define three columns equally spaced.
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                // Variant Name

                Variant variantName = await _variantRepository.GetVariantById(detail.VariantId);
                Debug.WriteLine(variantName.Storage);

                var variantText = new TextBlock
                {
                    Text = $"Variant: {detail.Quantity}",
                    FontSize = 18,
                    Margin = new Thickness(0, 0, 10, 0)
                };
                Grid.SetColumn(variantText, 0);

                // Quantity Column
                var quantityText = new TextBlock
                {
                    Text = $"Quantity: {detail.Quantity}",
                    FontSize = 18,
                    Margin = new Thickness(0, 0, 10, 0)
                };
                Grid.SetColumn(quantityText, 1);

                // Price Column
                var priceText = new TextBlock
                {
                    Text = $"Price: {detail.Price}",
                    FontSize = 18
                };
                Grid.SetColumn(priceText, 2);

                // Add the TextBlocks to the grid.
                grid.Children.Add(variantText);
                grid.Children.Add(quantityText);
                grid.Children.Add(priceText);

                // Add the grid to your main content container.
                content.Children.Add(grid);
            }


            //var customer = await _customerRepository.GetCustomerByIdAsync(sampleCustomerId);
            ContentDialog invoiceDetailDialog = new ContentDialog
            {
                Content = content,
                CloseButtonText = "ĐÓNG",
                FullSizeDesired = true,
                BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent),
                BorderThickness = new Thickness(10),
                RequestedTheme = ElementTheme.Light,
                XamlRoot = this.XamlRoot,
            };
            invoiceDetailDialog.Title = new StackPanel
            {
                Children =
                {
                    new TextBlock
                    {
                        Text = "CHI TIẾT HÓA ĐƠN",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 25,
                        FontWeight = FontWeights.Bold
                    }
                },
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };

            await invoiceDetailDialog.ShowAsync();
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
