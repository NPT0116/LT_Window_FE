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
using PhoneSelling.Data.Services.FileUpload;
using PhoneSelling.DependencyInjection;
using System.Threading.Tasks;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Pages.Items;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Controls
{
    public sealed partial class AddColorButton : UserControl // ✅ Must match XAML
    {
        private string _selectedImagePath;

        public event Action<string, string> ColorAdded;
        private readonly IUploadService _uploadService;

        public AddColorButton()
        {
            this.InitializeComponent();
            _uploadService = DIContainer.GetKeyedSingleton<IUploadService>();
        }

        private async void OnAddColorClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = new TempColor(); // ViewModel instance for validation

            var stackPanel = new StackPanel { Spacing = 10 };

            // Name input with validation
            var nameTextBox = new TextBox { PlaceholderText = "Nhập tên màu..." };
            nameTextBox.TextChanged += (s, args) =>
            {
                viewModel.Name = nameTextBox.Text; // Update ViewModel when user types
            };

            var nameErrorText = new TextBlock { Foreground = new SolidColorBrush(Colors.Red), Visibility = Visibility.Collapsed };

            var selectFileButton = new Button { Content = "Chọn File" };
            var previewImage = new Image { Height = 100, Width = 100, Visibility = Visibility.Collapsed };

            StorageFile selectedFile = null; // Store the selected image file

            selectFileButton.Click += async (s, args) =>
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker
                {
                    SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
                };
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".png");

                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

                selectedFile = await picker.PickSingleFileAsync();
                if (selectedFile != null)
                {
                    using (var stream = await selectedFile.OpenAsync(FileAccessMode.Read))
                    {
                        var bitmapImage = new BitmapImage();
                        await bitmapImage.SetSourceAsync(stream);
                        previewImage.Source = bitmapImage;
                        previewImage.Visibility = Visibility.Visible;
                    }
                }
            };

            stackPanel.Children.Add(new TextBlock { Text = "Tên Màu:" });
            stackPanel.Children.Add(nameTextBox);
            stackPanel.Children.Add(nameErrorText);
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

            while (true)
            {
                var result = await dialog.ShowAsync();
                if (result != ContentDialogResult.Primary)
                    return; // User cancelled

                // Validate input before proceeding
                viewModel.Name = nameTextBox.Text; // Ensure latest input is checked
                if (viewModel.HasErrors)
                {
                    var errors = viewModel.GetErrors(nameof(viewModel.Name)) as IEnumerable<string>;

                    if (errors != null)
                    {
                        nameErrorText.Text = string.Join("\n", errors);
                    }
                    else
                    {
                        nameErrorText.Text = "Unknown validation error.";
                    }

                    nameErrorText.Visibility = Visibility.Visible;
                    continue; // Keep showing the dialog until valid
                }

                if (selectedFile != null)
                {
                    // ✅ Assume we have an upload service
                    var fileBytes = await ConvertStorageFileToByteArray(selectedFile);
                    var request = new MediaUploadRequest("", fileBytes, "test", Media.GetMimeType(selectedFile.Name));
                    var uploadedFile = await _uploadService.UploadFileAsync(request, System.Threading.CancellationToken.None);

                    // ✅ Notify the ViewModel (e.g., PhonePageViewModel.OnColorAdded)
                    ColorAdded?.Invoke(nameTextBox.Text, uploadedFile.Url);
                }
                break;
            }
        }


        private async Task<byte[]> ConvertStorageFileToByteArray(StorageFile file)
        {
            using (var stream = await file.OpenStreamForReadAsync())
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray(); // ✅ Return byte array
            }
        }


    }
}
