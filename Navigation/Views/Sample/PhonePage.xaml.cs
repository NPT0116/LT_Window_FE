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
using PhoneSelling.Data.Repositories.PhoneRepository;
using PhoneSelling.ViewModel.Pages.Sample;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhonePage : Page
    {
        private readonly ItemViewModel _viewModel;
        public PhonePage()
        {
            this.InitializeComponent();
            _viewModel = new ItemViewModel();
            this.DataContext = _viewModel;
        }

        private void navigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            
        }

        private async void OnAddColorClicked(object sender, RoutedEventArgs e)
        {
            var stackPanel = new StackPanel { Spacing = 10 };

            var nameTextBox = new TextBox { PlaceholderText = "Nhập tên màu..." };
            var selectFileButton = new Button { Content = "Chọn File" };
            var previewImage = new Image { Height = 100, Width = 100, Visibility = Microsoft.UI.Xaml.Visibility.Collapsed };
            string _selectedImagePath = "";
            selectFileButton.Click += async (s, args) =>
            {
                var picker = new FileOpenPicker
                {
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary
                };
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".png");

                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

                StorageFile file = await picker.PickSingleFileAsync();
                
                if (file != null)
                {
                    _selectedImagePath = file.Path; // Store the selected file path
                    var bitmapImage = new BitmapImage(new Uri(file.Path));
                    previewImage.Source = bitmapImage;
                    previewImage.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                }
            };

            stackPanel.Children.Add(new TextBlock { Text = "Tên Màu:" });
            stackPanel.Children.Add(nameTextBox);
            stackPanel.Children.Add(new TextBlock { Text = "Chọn Hình Ảnh:" });
            stackPanel.Children.Add(selectFileButton);
            stackPanel.Children.Add(previewImage);

            var dialog = new ContentDialog
            {
                Title = "Thêm Màu",
                Content = stackPanel,
                PrimaryButtonText = "Lưu",
                CloseButtonText = "Hủy",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // Call ViewModel Command on Accept, passing the selected image path
                _viewModel.AddColor((nameTextBox.Text, _selectedImagePath));
            }
        }
    }
}
