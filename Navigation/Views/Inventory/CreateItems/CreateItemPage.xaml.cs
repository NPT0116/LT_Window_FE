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
using System.Linq;

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
            var panel = new StackPanel { Spacing = 20 };

            // TextBlock and TextBox for the color name.
            panel.Children.Add(new TextBlock { Text = "Tên màu:" });
            var nameTextBox = new TextBox { PlaceholderText = "Nhập tên màu", Width = 300 };
            panel.Children.Add(nameTextBox);

            // Button to select image.
            var selectImageButton = new Button { Content = "Chọn hình ảnh", Width = 300 };
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

            // Create and show the dialog.
            var dialog = new ContentDialog
            {
                Title = "THÊM MÀU SẮC",
                Content = panel,
                PrimaryButtonText = "Tạo",
                CloseButtonText = "Hủy",
                XamlRoot = this.XamlRoot,
                RequestedTheme = ElementTheme.Light,
                IsPrimaryButtonEnabled = false,
            };

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

                    // Now that the image is loaded, enable the Save button.
                    dialog.IsPrimaryButtonEnabled = true;
                }
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

            // Get the view model from the DataContext.
            if (this.DataContext is CreateItemPageViewModel viewModel)
            {
                // Check for duplicate color names (case-insensitive).
                bool duplicateExists = viewModel.ColorList.Any(c =>
                    string.Equals(c, colorName, StringComparison.OrdinalIgnoreCase));

                if (duplicateExists)
                {
                    // Show an error dialog if duplicate found.
                    var errorDialog = new ContentDialog
                    {
                        Title = "Duplicate Color",
                        Content = $"A variant with the color name '{colorName}' already exists.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await errorDialog.ShowAsync();
                    return;
                }

                // Create a new TempColor instance using the provided name and uploaded image URL.
                var tempColor = new TempColor
                {
                    TempId = viewModel.ColorList.Count + 1,
                    Name = colorName,
                    UrlImage = imageUrl ?? string.Empty,
                    ColorId = Guid.NewGuid()
                };

                // Add the new TempColor to the ViewModel's ColorList.
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

            
            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);
            // PreviewImage
            var previewImage = new Image { Width = 100, Height = 100, Visibility = Visibility.Collapsed};
            // ProgressRing
            var progressRing = new ProgressRing { Width = 100, Height = 100 , Visibility = Visibility.Collapsed};
            PictureContainer.Children.Add(previewImage);
            PictureContainer.Children.Add(progressRing);


            StorageFile selectedFile = await picker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                // Preview Image
                using (var stream = await selectedFile.OpenAsync(FileAccessMode.Read))
                {
                    var bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(stream);
                    previewImage.Source = bitmapImage;
                    previewImage.Visibility = Visibility.Visible;
                }
                // Ring
                progressRing.Visibility = Visibility.Visible;
                progressRing.IsActive = true;
                
                // Archieve URL
                byte[] fileBytes = await ConvertStorageFileToByteArray(selectedFile);
                var request = new MediaUploadRequest("", fileBytes, selectedFile.Name, Media.GetMimeType(selectedFile.Name));
                var uploadedFile = await _uploadService.UploadFileAsync(request, System.Threading.CancellationToken.None);
                Debug.WriteLine(uploadedFile.Url);

                progressRing.Visibility = Visibility.Collapsed;
                progressRing.IsActive = false;
                previewImage.Visibility = Visibility.Collapsed;

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

            // Check for duplicate in the current Viemodel item group
            if (this.DataContext is CreateItemPageViewModel viewModel)
            {
                bool duplicateExists = viewModel.ItemGroups.Any(ig => string.Equals(ig.ItemGroupName, groupName, StringComparison.OrdinalIgnoreCase));

                if (duplicateExists)
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Duplicate Item Group",
                        Content = $"An item group with the name '{groupName}' already exists.",
                        CloseButtonText="Close",
                        XamlRoot = this.XamlRoot,
                    };
                    await errorDialog.ShowAsync();
                    return;
                }
            }
            var request = new CreateItemGroupRequest
            {
                itemGroupName = groupName
            };
            // Retrieve the repository from DI.
            var itemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>();
            var newGroup = await itemGroupRepository.CreateItemGroupAsync(request);

            if (newGroup != null)
            {
                if (this.DataContext is CreateItemPageViewModel viewModel1)
                {
                    Debug.WriteLine("New Group Added: " + newGroup.ItemGroupName);
                    viewModel1.ItemGroups.Add(newGroup);
                    // Optionally, set the new group as selected.
                    viewModel1.ItemGroups = new List<ItemGroup>(viewModel1.ItemGroups);
                    viewModel1.SelectedItemGroup = newGroup;
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

            // Check for duplicate in the current view model's manufacturers.
            if (this.DataContext is CreateItemPageViewModel viewModel)
            {
                bool duplicateExists = viewModel.Manufacturers.Any(m =>
                    string.Equals(m.ManufacturerName, manufacturerName, StringComparison.OrdinalIgnoreCase));

                if (duplicateExists)
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Duplicate Manufacturer",
                        Content = $"A manufacturer with the name '{manufacturerName}' already exists.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await errorDialog.ShowAsync();
                    return;
                }
            }

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
                if (this.DataContext is CreateItemPageViewModel viewModel1)
                {
                    Debug.WriteLine("New Manufacturer Added: " + newManufacturer.ManufacturerName);
                    viewModel1.Manufacturers.Add(newManufacturer);
                    // Optionally refresh the list.
                    viewModel1.Manufacturers = new List<Manufacturer>(viewModel1.Manufacturers);
                    viewModel1.SelectedManufacturer = newManufacturer;
                }
            }
            else
            {
                Debug.WriteLine("Failed to create new manufacturer.");
            }
        }
        // Seach box
        private void manufacturerSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (this.DataContext is CreateItemPageViewModel viewModel)
                {
                    viewModel.FilterManufacturers(sender.Text);
                }
            }
        }
    }
}
