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
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Controls
{
    public sealed partial class AddColorButton : UserControl // ✅ Must match XAML
    {
        private string _selectedImagePath;

        public event Action<string, string> ColorAdded;

        public AddColorButton()
        {
            this.InitializeComponent();
        }

        private async void OnAddColorClicked(object sender, RoutedEventArgs e)
        {
            var stackPanel = new StackPanel { Spacing = 10 };
            var nameTextBox = new TextBox { PlaceholderText = "Nhập tên màu..." };
            var selectFileButton = new Button { Content = "Chọn File" };
            var previewImage = new Image { Height = 100, Width = 100, Visibility = Visibility.Collapsed };

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
                    _selectedImagePath = file.Path;
                    var bitmapImage = new BitmapImage(new Uri(file.Path));
                    previewImage.Source = bitmapImage;
                    previewImage.Visibility = Visibility.Visible;
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
                ColorAdded?.Invoke(nameTextBox.Text, _selectedImagePath);
            }
        }
    }
}
