using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.IO;
using System.Threading.Tasks;
using PhoneSelling.Data.Services.FileUpload;
using PhoneSelling.DependencyInjection;
using WinRT.Interop; // For GetWindowHandle and InitializeWithWindow
using PhoneSelling.ViewModel.Pages.Inventory.CreateItemPages;
using PhoneSelling.Data.Models;
using System;
using System.Diagnostics;

namespace Navigation.Views.Inventory.CreateItems
{
    public sealed partial class CreateItemPage : Page
    {
        public CreateItemPageViewModel ViewModel { get; } = new CreateItemPageViewModel();
        private readonly IUploadService _uploadService;

        public CreateItemPage()
        {
            Debug.WriteLine("ViewModel is create");
            this.InitializeComponent();
            this.DataContext = ViewModel;
            _uploadService = DIContainer.GetKeyedSingleton<IUploadService>();
        }

        // This event handler is triggered by a button in your XAML (e.g., "Select Image" button).
        private async void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            // Create a FileOpenPicker to select image files.
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            // For WinUI 3, set the window handle.
            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);

            // Let the user pick a single file.
            StorageFile selectedFile = await picker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                // Convert the selected file to a byte array.
                byte[] fileBytes = await ConvertStorageFileToByteArray(selectedFile);

                // Create a MediaUploadRequest (adjust parameters as needed).
                var request = new MediaUploadRequest("", fileBytes, selectedFile.Name, Media.GetMimeType(selectedFile.Name));

                // Upload the file via your upload service.
                var uploadedFile = await _uploadService.UploadFileAsync(request, System.Threading.CancellationToken.None);
                Debug.WriteLine(uploadedFile.Url);

                // Use the same instance (DataContext as CreateItemPageViewModel)
                if (this.DataContext is CreateItemPageViewModel viewModel)
                {
                    viewModel.Picture = uploadedFile.Url;
                }
            }
        }

        // Helper method to convert a StorageFile to a byte array.
        private async Task<byte[]> ConvertStorageFileToByteArray(StorageFile file)
        {
            using (var stream = await file.OpenStreamForReadAsync())
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
