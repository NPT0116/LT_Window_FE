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

using Microsoft.Win32;

using Navigation.Views;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Text;
using PhoneSelling.Data.Repositories.InvoiceRepository;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using Navigation.Converters;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Amazon.Runtime.Internal.Util;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService;
using System.Text.Json;

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
        private IInvoiceRepository _invoiceRepository { get; set; }
        public InvoiceListPage()
        {
            this.InitializeComponent();
            ViewModel = new InvoiceListViewModel();
            this.DataContext = ViewModel;
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();
            _invoiceRepository = DIContainer.GetKeyedSingleton<IInvoiceRepository>();
        }


        private async void GoToInvoiceDetailPage(object sender, RoutedEventArgs args)
        {
            var currencyConverter = new VndCurrencyConverter();

            var button = sender as Button;
            var invoiceInfor = (Invoice)button.DataContext;
            var customerName = App.CustomerDictionary.TryGetValue(invoiceInfor.CustomerID, out var value) ? value : "";

            var convertedTotal = currencyConverter.Convert(invoiceInfor.TotalAmount, typeof(string), null, "vi-VN") as string;

            // Main container grid with three rows:
            // Row 0: General invoice info (ID, Date, Customer, etc.)
            // Row 1: Invoice details area (header fixed + scrollable content)
            // Row 2: Total panel
            var mainContain = new Grid();
            mainContain.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainContain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainContain.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // A. Invoice Info Section
            var invoiceInfoPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Padding = new Thickness(10)
            };
            invoiceInfoPanel.Children.Add(new TextBlock
            {
                Text = invoiceInfor.InvoiceID.ToString(),
                FontSize = 22,
                TextWrapping = TextWrapping.Wrap
            });
            invoiceInfoPanel.Children.Add(new TextBlock
            {
                Text = invoiceInfor.Date.ToString("dd/MM/yyyy"),
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Right,
            });
            invoiceInfoPanel.Children.Add(new StackPanel
            {
                Children =
                {
                    new TextBlock { Text = "1. Tên khách hàng:", FontWeight = FontWeights.Bold },
                    new TextBlock { Text = customerName, FontSize = 18, Margin= new Thickness(20,5,0,10) }

                },
                Orientation = Orientation.Vertical,
            });
            invoiceInfoPanel.Children.Add(new StackPanel
            {
                Children =
                {
                    new TextBlock { Text = "2. Danh sách mua hàng:", FontWeight = FontWeights.Bold }
                }
            });
            Grid.SetRow(invoiceInfoPanel, 0);
            mainContain.Children.Add(invoiceInfoPanel);

            // B. Invoice Detail Section: Create a Grid with two rows
            var invoiceDetailGrid = new Grid();
            invoiceDetailGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Header row
            invoiceDetailGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Scrollable content

            // B.1 Header (static)
            var gridHeader = new Grid
            {
                Background = new SolidColorBrush(Colors.DarkSlateGray),
                Padding = new Thickness(7)
            };
            gridHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            gridHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var productHeader = new TextBlock
            {
                Text = "Sản phẩm",
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),

                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(productHeader, 0);

            var quantityHeader = new TextBlock
            {
                Text = "Số lượng",
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),

                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(quantityHeader, 1);

            var sellingPriceHeader = new TextBlock
            {
                Text = "Giá bán",
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),

                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(sellingPriceHeader, 2);

            var totalHeader = new TextBlock
            {
                Text = "Thành tiền",
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),

                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(totalHeader, 3);

            gridHeader.Children.Add(productHeader);
            gridHeader.Children.Add(quantityHeader);
            gridHeader.Children.Add(sellingPriceHeader);
            gridHeader.Children.Add(totalHeader);

            Grid.SetRow(gridHeader, 0);
            invoiceDetailGrid.Children.Add(gridHeader);

            // B.2 Scrollable Invoice Detail Rows
            var detailRowsPanel = new StackPanel { 
                Padding  = new Thickness(10,0,10,0)
            };
            int index = 1;
            foreach (var detail in invoiceInfor.InvoiceDetails)
            {
                // Create a row grid for each detail
                var detailGrid = new Grid { Margin = new Thickness(0, 5, 0, 5) };
                detailGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                detailGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                detailGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                detailGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                // Retrieve variant details asynchronously
                Variant variantDetail = await _variantRepository.GetVariantById(detail.VariantId);
                var variantName = $"{index++}. {variantDetail.Item.ItemName} - {variantDetail.Color.Name} - {variantDetail.Storage}";

                var variantText = new TextBlock
                {
                    Text = variantName,
                    FontSize = 15,
    
                    TextWrapping = TextWrapping.Wrap,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(variantText, 0);

                var quantityText = new TextBlock
                {
                    Text = $"x{detail.Quantity}",
                    FontSize = 15,
    
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(quantityText, 1);

                var priceText = new TextBlock
                {
                    Text = currencyConverter.Convert(detail.Price, typeof(string), null, "vi-VN") as string,
                    FontSize = 15,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(priceText, 2);

                var totalText = new TextBlock
                {
                    Text = currencyConverter.Convert(detail.Total, typeof(string), null, "vi-VN") as string,
                    FontSize = 15,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(totalText, 3);

                detailGrid.Children.Add(variantText);
                detailGrid.Children.Add(quantityText);
                detailGrid.Children.Add(priceText);
                detailGrid.Children.Add(totalText);

                detailRowsPanel.Children.Add(detailGrid);
            }

            // Wrap the detail rows in a ScrollViewer so they scroll independently of the header.
            var scrollViewer = new ScrollViewer
            {
                Content = detailRowsPanel,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
            };
            Grid.SetRow(scrollViewer, 1);
            invoiceDetailGrid.Children.Add(scrollViewer);

            // Add the invoice detail grid to main container (row 1)
            Grid.SetRow(invoiceDetailGrid, 1);
            mainContain.Children.Add(invoiceDetailGrid);

            // C. Total Panel (static at the bottom)
            var totalPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            totalPanel.Children.Add(new TextBlock
            {
                Text = "TỔNG HÓA ĐƠN",
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            totalPanel.Children.Add(new TextBlock
            {
                Text = convertedTotal,
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            totalPanel.Children.Add(new TextBlock
            {
                Text = "VNĐ",
                FontSize = 15,
                TextDecorations = TextDecorations.Underline,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            Grid.SetRow(totalPanel, 2);
            mainContain.Children.Add(totalPanel);

            // Create and show the ContentDialog.
            var contentWrapper = new Border
            {
                Width = 500,
                Padding=new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                Child = mainContain
            };
            var invoiceDetailDialog = new ContentDialog
            {
                Content = contentWrapper,
                CloseButtonText = "ĐÓNG",
                
                FullSizeDesired = true,
                BorderBrush = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(10),
                RequestedTheme = ElementTheme.Light,
                XamlRoot = this.XamlRoot,
            };

            // Set a centered title for the dialog.
            invoiceDetailDialog.Title = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Children =
                {
                    new TextBlock
                    {
                        Text = "CHI TIẾT HÓA ĐƠN",
                        FontSize = 25,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center
                    }
                }
            };

            await invoiceDetailDialog.ShowAsync();
        }

        private async void ExportInvoice(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            var invoiceInfor = (Invoice)button.DataContext;
            Debug.WriteLine("Processing Export Invoice");

            try
            {
                // 1. Get the PDF content from
                byte[] pdfContent = await _invoiceRepository.GetInvoicePdfPrintElectronicAsync(invoiceInfor.Id);

                // 2. Create the FileSavePicker
                var savePicker = new Windows.Storage.Pickers.FileSavePicker
                {
                    SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
                };
                savePicker.FileTypeChoices.Add("PDF files", new List<string> { ".pdf" });
                savePicker.SuggestedFileName = "Invoice";

                // 3.Use the Window handle, not the Page handle
                var window = App.MainWindow;
                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hWnd);

                // 4. Let the user pick a location to save
                Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    await Windows.Storage.FileIO.WriteBytesAsync(file, pdfContent);

                    // 5. Show success dialog
                    var successDialog = new ContentDialog
                    {
                        Title = "THÀNH CÔNG",
                        Content = $"PDF được lưu thành công tại {file.Path}",
                        CloseButtonText = "Đóng",
                        XamlRoot = this.XamlRoot,
                        RequestedTheme  = ElementTheme.Light
                    };
                    await successDialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                // Show error dialog
                var errorDialog = new ContentDialog
                {
                    Title = "LỖI",
                    Content = "Lỗi khi lưu hóa đơn: " + ex.Message,
                    CloseButtonText = "Đóng",
                    XamlRoot = this.XamlRoot,
                    RequestedTheme = ElementTheme.Light
                };
                await errorDialog.ShowAsync();
            }
        }



        private void CalendarFromDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate.HasValue)
            {
                var selectedDate = args.NewDate.Value.Date;
                var startOfDay = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 0, 0, 0, DateTimeKind.Utc);
                Debug.WriteLine(startOfDay);
                ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.FromDate = startOfDay;
            }
        }
        private void CalendarToDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (args.NewDate.HasValue)
            {
                var selectedDate = args.NewDate.Value.Date;
                var endOfDay = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 23,59,59, DateTimeKind.Utc);
                Debug.WriteLine(endOfDay);
                ViewModel.InvoiceQuery.Query.InvoiceDatetimeQueryParameter.ToDate = endOfDay;
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
