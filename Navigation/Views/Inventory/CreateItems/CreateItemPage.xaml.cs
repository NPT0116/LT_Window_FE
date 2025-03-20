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
using PhoneSelling.Data.Repositories.ItemGroupRepository;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using System.Collections.Generic;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ManufacturerRepository;
using PhoneSelling.ViewModel.Pages.Items;

namespace Navigation.Views.Inventory.CreateItems
{
    public sealed partial class CreateItemPage : Page
    {
        public CreateItemPageViewModel ViewModel { get; } = new CreateItemPageViewModel();
        private readonly IUploadService _uploadService;

        public CreateItemPage()
        {
            Debug.WriteLine("ViewModel is created");
            this.InitializeComponent();
            this.DataContext = ViewModel;
            _uploadService = DIContainer.GetKeyedSingleton<IUploadService>();
        }


        private async void AddNewColor_Click(object sender, RoutedEventArgs e)
        {
            // Create a panel that will act as our mini form.
            var panel = new StackPanel { Spacing = 10 };

            // TextBlock and TextBox for the color name.
            panel.Children.Add(new TextBlock { Text = "Color Name:" });
            var nameTextBox = new TextBox { PlaceholderText = "Enter color name", Width = 300 };
            panel.Children.Add(nameTextBox);

            // Button to select image.
            var selectImageButton = new Button { Content = "Select Image", Width = 150 };
            panel.Children.Add(selectImageButton);

            // Image preview.
            var previewImage = new Image { Width = 100, Height = 100, Visibility = Visibility.Collapsed };
            panel.Children.Add(previewImage);

            // Progress indicator.
            var progressRing = new ProgressRing
            {
                Width = 30,
                Height = 30,
                IsActive = false,
                Visibility = Visibility.Collapsed
            };
            panel.Children.Add(progressRing);

            // Local variable to hold the uploaded image URL.
            string imageUrl = null;
            StorageFile selectedFile = null;

            // Get the upload service instance.
            var uploadService = DIContainer.GetKeyedSingleton<IUploadService>();

            // When the "Select Image" button is clicked, allow the user to pick and upload an image.
            selectImageButton.Click += async (s, args) =>
            {
                var picker = new FileOpenPicker
                {
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary
                };
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".png");

                // Set the window handle (WinUI 3 requirement)
                var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
                InitializeWithWindow.Initialize(picker, hwnd);

                selectedFile = await picker.PickSingleFileAsync();
                if (selectedFile != null)
                {
                    // Display preview.
                    using (var stream = await selectedFile.OpenAsync(FileAccessMode.Read))
                    {
                        var bitmapImage = new BitmapImage();
                        await bitmapImage.SetSourceAsync(stream);
                        previewImage.Source = bitmapImage;
                        previewImage.Visibility = Visibility.Visible;
                    }

                    // Show progress and disable the button.
                    progressRing.IsActive = true;
                    progressRing.Visibility = Visibility.Visible;
                    selectImageButton.IsEnabled = false;

                    // Convert file to byte array.
                    byte[] fileBytes = await ConvertStorageFileToByteArray(selectedFile);
                    var request = new MediaUploadRequest("", fileBytes, selectedFile.Name, Media.GetMimeType(selectedFile.Name));
                    var uploadedFile = await uploadService.UploadFileAsync(request, System.Threading.CancellationToken.None);
                    imageUrl = uploadedFile.Url;
                    Debug.WriteLine("Uploaded image URL: " + imageUrl);

                    // Hide progress and re-enable button.
                    progressRing.IsActive = false;
                    progressRing.Visibility = Visibility.Collapsed;
                    selectImageButton.IsEnabled = true;
                }
            };

            // Create and show the dialog.
            var dialog = new ContentDialog
            {
                Title = "Add New Color",
                Content = panel,
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
                return;

            var colorName = nameTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(colorName))
            {
                Debug.WriteLine("Color name is required.");
                return;
            }

            // Create a new TempColor instance using the provided name and uploaded image URL.
            var tempColor = new TempColor
            {
                TempId = (this.DataContext as CreateItemPageViewModel)?.ColorList.Count + 1 ?? 1,
                Name = colorName,
                UrlImage = imageUrl ?? string.Empty,
                ColorId = Guid.NewGuid()
            };

            // Add the new TempColor to the ViewModel's ColorList.
            if (this.DataContext is CreateItemPageViewModel viewModel)
            {
                viewModel.ColorList.Add(tempColor.Name);
                viewModel.ColorObjectList.Add(tempColor);
                viewModel.UpdateVariants();
                Debug.WriteLine("New color added: " + tempColor.Name);
            }
        }

        // This event handler is triggered by the "Select Image" button.
        private async void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            // For WinUI 3, set the window handle.
            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile selectedFile = await picker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                byte[] fileBytes = await ConvertStorageFileToByteArray(selectedFile);
                var request = new MediaUploadRequest("", fileBytes, selectedFile.Name, Media.GetMimeType(selectedFile.Name));
                var uploadedFile = await _uploadService.UploadFileAsync(request, System.Threading.CancellationToken.None);
                Debug.WriteLine(uploadedFile.Url);

                // Ensure we update the same ViewModel instance bound to the UI.
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

        // Event handler to add a new Item Group.
        private async void AddNewGroup_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel { Spacing = 10 };
            var groupNameTextBox = new TextBox { PlaceholderText = "Enter group name", Width = 300 };
            panel.Children.Add(groupNameTextBox);

            var dialog = new ContentDialog
            {
                Title = "Add New Item Group",
                Content = panel,
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
                return;

            var groupName = groupNameTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(groupName))
                return;

            var request = new CreateItemGroupRequest
            {
                itemGroupName = groupName
            };

            // Retrieve the repository from DI.
            var itemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>();
            var newGroup = await itemGroupRepository.CreateItemGroupAsync(request);

            if (newGroup != null)
            {
                if (this.DataContext is CreateItemPageViewModel viewModel)
                {
                    Debug.WriteLine("New Group Added: " + newGroup.ItemGroupName);
                    viewModel.ItemGroups.Add(newGroup);
                    // Optionally, set the new group as selected.
                    //viewModel.SelectedItemGroup = newGroup;
                }
            }
            else
            {
                Debug.WriteLine("Failed to create new group.");
            }
        }

        // Event handler to add a new Manufacturer.
        private async void AddNewManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel { Spacing = 10 };
            var manufacturerNameTextBox = new TextBox { PlaceholderText = "Enter manufacturer name", Width = 300 };
            var manufacturerDescriptionTextBox = new TextBox { PlaceholderText = "Enter manufacturer description", Width = 300 };
            panel.Children.Add(manufacturerNameTextBox);
            panel.Children.Add(manufacturerDescriptionTextBox);

            var dialog = new ContentDialog
            {
                Title = "Add New Manufacturer",
                Content = panel,
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
                return;

            var manufacturerName = manufacturerNameTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(manufacturerName))
                return;
            var manufacturerDescription = manufacturerDescriptionTextBox.Text?.Trim();

            var request = new CreateManufacturerRequest
            {
                ManufacturerName = manufacturerName,
                ManufacturerDescription = manufacturerDescription
            };

            // Retrieve the manufacturer repository from DI.
            var manufacturerRepository = DIContainer.GetKeyedSingleton<IManufacturerRepository>();
            var newManufacturer = await manufacturerRepository.CreateManufacturerAsync(request);

            if (newManufacturer != null)
            {
                if (this.DataContext is CreateItemPageViewModel viewModel)
                {
                    Debug.WriteLine("New Manufacturer Added: " + newManufacturer.ManufacturerName);
                    viewModel.Manufacturers.Add(newManufacturer);
                    // Optionally, set it as selected.
                    //viewModel.SelectedManufacturer = newManufacturer;
                }
            }
            else
            {
                Debug.WriteLine("Failed to create new manufacturer.");
            }
        }
    }
}
