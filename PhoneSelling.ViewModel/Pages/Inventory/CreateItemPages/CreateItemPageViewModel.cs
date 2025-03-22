using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;
using PhoneSelling.DependencyInjection;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.ItemGroupRepository;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ManufacturerRepository;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Query;
using PhoneSelling.ViewModel.Pages.Items;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace PhoneSelling.ViewModel.Pages.Inventory.CreateItemPages
{
    // Inherit from ObservableValidator to support data annotation validation.
    public partial class CreateItemPageViewModel : BasePageViewModel
    {
        // Use the validation attributes to define required fields.
        [ObservableProperty]
        [Required(ErrorMessage = "Item Name is required.")]
        private string itemName;
        partial void OnItemNameChanged(string oldValue, string newValue)
        {
            ValidateProperty(newValue, nameof(ItemName));
            OnPropertyChanged(nameof(ItemNameError));
        }
        public string ItemNameError => GetErrors(nameof(ItemName))?.Cast<object>()?.FirstOrDefault()?.ToString() ?? string.Empty;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private string? picture;

        [ObservableProperty]
        private DateTime? releaseDate = DateTime.UtcNow;

        [ObservableProperty]
        private ObservableCollection<Variant> variants;
        public string VariantError => CheckVariant || (Variants == null || Variants.Count > 0) ? string.Empty : "At least one variant is required.";
        private bool _checkVariant = true;
        public bool CheckVariant
        {
            get => _checkVariant;
            set => SetProperty(ref _checkVariant, value);
        }

        [ObservableProperty]
        private Guid? itemGroupId;

        [ObservableProperty]
        private Guid manufacturerId;

        // Selected fields
        [ObservableProperty]
        [Required(ErrorMessage ="Item Group is required.")]
        private ItemGroup selectedItemGroup;
        partial void OnSelectedItemGroupChanged(ItemGroup oldValue, ItemGroup newValue)
        {
            ValidateProperty(newValue, nameof(SelectedItemGroup));
            OnPropertyChanged(nameof(ItemGroupError));
        }
        public string ItemGroupError => GetErrors(nameof(SelectedItemGroup))?.Cast<object>()?.FirstOrDefault()?.ToString() ?? String.Empty;

        [ObservableProperty]
        private Manufacturer selectedManufacturer;

        // Manually add Storage and Color
        // Storage
        public ObservableCollection<string> StorageList { get; } = new ObservableCollection<string>();
        private string _newStorage;
        public string NewStorage
        {
            get => _newStorage;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Trim();
                    var match = Regex.Match(value, @"^(\d+)(GB)?$", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        string numberPart = match.Groups[1].Value;
                        value = numberPart + "GB";
                        SetProperty(ref _newStorage, value);
                    } else if (!value.EndsWith("GB", StringComparison.OrdinalIgnoreCase) && int.TryParse(value, out int number))
                    {
                        value = $"{number}GB";
                        SetProperty(ref _newStorage, value);
                    }
                } 
                else
                {
                    SetProperty(ref _newStorage, value);
                }
            }
        }
        // Color
        public ObservableCollection<string> ColorList { get; } = new ObservableCollection<string>();
        public ObservableCollection<TempColor> ColorObjectList { get; } = new ObservableCollection<TempColor>();
        private string _newColor;
        public string NewColor
        {
            get => _newColor;
            set => SetProperty(ref _newColor, value);
        }

        // Fetch data for item group and manufacturer
        [ObservableProperty]
        private List<ItemGroup> itemGroups = new();
        public async Task LoadItemGroupsAsync()
        {
            var groups = await _itemGroupRepository.GetItemGroupsAsync(new ItemGroupQueryParameter());
            if (groups != null)
            {
                itemGroups.Clear();
                foreach (var group in groups.Data)
                {
                    itemGroups.Add(group);
                }
            }
        }

        [ObservableProperty]
        private List<Manufacturer> manufacturers = new();
        public async Task LoadManufacturerAsync()
        {
            var manufacturersResponse = await _manufacturerRepository.GetManufacturersAsync(new ManufacturerQueryParameter());
            if (manufacturersResponse != null)
            {
                manufacturers.Clear();
                foreach (var manufacturer in manufacturersResponse.Data)
                {
                    manufacturers.Add(manufacturer);
                }
            }
        }

        // Repository fields (retrieved via DI)
        private readonly IItemRepository _itemRepository;
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public CreateItemPageViewModel()
        {
            // Initialize properties
            itemName = string.Empty;
            description = string.Empty;
            picture = string.Empty;
            _newStorage = string.Empty;
            _newColor = string.Empty;
            variants = new ObservableCollection<Variant>();
            variants.CollectionChanged += Variants_CollectionChanged;

            // Retrieve repositories via dependency injection.
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            _itemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>();
            _manufacturerRepository = DIContainer.GetKeyedSingleton<IManufacturerRepository>();

            _ = LoadItemGroupsAsync();
            _ = LoadManufacturerAsync();
        }
        private void Variants_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) 
        {
            CheckVariant = false;
            ValidateFields();
            OnPropertyChanged(nameof(VariantError));
        }
        // Validation error message to display on the UI.
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
        // Validate fields using ObservableValidator's built-in functionality.
        public bool ValidateFields()
        {
            // Clear previous errors.
            ErrorMessage = string.Empty;
            CheckVariant = false;
            ValidateAllProperties();
            UpdateUI();

            if (HasErrors)
            {
                // Aggregate all error messages.
                var errors = GetErrors(null)
                    .Cast<object>()
                    .Select(e => e.ToString())
                    .ToList();
                ErrorMessage = string.Join(Environment.NewLine, errors);
                Debug.WriteLine("Validation Errors: ");
                Debug.WriteLine(ErrorMessage);
                return false;
            }

            return true;
        }
        private void UpdateUI()
        {
            OnPropertyChanged(nameof(ItemNameError));
            OnPropertyChanged(nameof(VariantError));
            OnPropertyChanged(nameof(ItemGroupError));
        }


        [RelayCommand]
        private void CreateVariant()
        {
            // Ensure there is at least one color in the ColorObjectList.
            var defaultColor = ColorObjectList.FirstOrDefault();
            if (defaultColor != null)
            {
                variants.Add(new Variant
                {
                    Storage = "0",
                    Color = new Color { Name = defaultColor.Name },
                    CostPrice = 0,
                    SellingPrice = 0,
                    StockQuantity = 0
                });
            }
        }

        [RelayCommand]
        private void RemoveVariant(Variant variant)
        {
            if (variant != null && variants.Contains(variant))
            {
                variants.Remove(variant);
                Debug.WriteLine("Variant removed.");
            }
        }

        [RelayCommand]
        private void AddNewStorage()
        {
            if (!string.IsNullOrEmpty(NewStorage) && !StorageList.Contains(NewStorage))
            {
                StorageList.Add(NewStorage);
                UpdateVariants();
            }
        }

        [RelayCommand]
        private void DeleteSelectedStorage(string storage)
        {
            if (!string.IsNullOrEmpty(storage) && StorageList.Contains(storage))
            {
                StorageList.Remove(storage);
            }
        }

        [RelayCommand]
        private void DeleteSelectedColor(string color)
        {
            if (!string.IsNullOrEmpty(color) && ColorList.Contains(color))
            {
                ColorList.Remove(color);
            }
        }

        // Update variants list based on the StorageList and ColorList.
        public void UpdateVariants()
        {
            foreach (var storage in StorageList)
            {
                foreach (var color in ColorList)
                {
                    bool exists = variants.Any(v =>
                        v.Storage == storage &&
                        v.Color != null &&
                        v.Color.Name.Equals(color, StringComparison.OrdinalIgnoreCase));
                    if (!exists)
                    {
                        var newVariant = new Variant
                        {
                            Storage = storage,
                            Color = new Color { Name = color },
                            CostPrice = 0,
                            SellingPrice = 0,
                            StockQuantity = 0,
                        };
                        variants.Add(newVariant);
                    }
                }
            }
        }

        [RelayCommand]
        private async Task SaveItem()
        {
            // Run validation before attempting to save.
            if (!ValidateFields())
            {
                Debug.WriteLine("Validation failed. Please check the entered data.");
                return;
            }

            Debug.WriteLine("Mapping ItemDto...");

            // Map item details to the ItemDto.
            var itemDto = new ItemDto
            {
                itemID = Guid.NewGuid().ToString(),
                itemGroupID = selectedItemGroup?.Id.ToString(),
                itemName = itemName,
                description = description,
                picture = picture,
                releaseDate = releaseDate?.ToString("o"),
                manufacturerID = selectedManufacturer?.Id.ToString()
            };

            Debug.WriteLine($"ItemDto mapped: {itemDto.itemName}, ReleaseDate: {itemDto.releaseDate}");

            // Map ColorObjectList to ColorDto objects.
            var colorDtos = ColorObjectList.Select((c, index) => new Data.Repositories.ItemRepository.ApiService.Contracts.Requests.ColorDto
            {
                tempId = index + 1,
                name = c.Name,
                urlImage = c.UrlImage,
                colorId = Guid.NewGuid().ToString()
            }).ToList();

            Debug.WriteLine("ColorDtos mapped:");
            foreach (var colorDto in colorDtos)
            {
                Debug.WriteLine($"TempId: {colorDto.tempId}, Name: {colorDto.name}");
            }

            // Map variants to VariantDto objects.
            var variantDtos = variants.Select(v => new VariantDto
            {
                storage = v.Storage,
                costPrice = (int)v.CostPrice,
                sellingPrice = (int)v.SellingPrice,
                stockQuantity = v.StockQuantity,
                colorTempId = colorDtos.FirstOrDefault(cd => cd.name == v.Color.Name)?.tempId ?? 0
            }).ToList();

            Debug.WriteLine("VariantDtos mapped:");
            foreach (var variantDto in variantDtos)
            {
                Debug.WriteLine($"Storage: {variantDto.storage}, Cost: {variantDto.costPrice}, ColorTempId: {variantDto.colorTempId}");
            }

            // Create the full DTO object.
            var createFullItemDto = new CreateFullItemDto
            {
                Item = itemDto,
                Colors = colorDtos,
                Variants = variantDtos
            };

            Debug.WriteLine("Calling _itemRepository.CreateFullItem...");
            // Save the item via the repository.
            await _itemRepository.CreateFullItem(createFullItemDto);
            Debug.WriteLine("Item saved successfully.");
        }

        // Search Box functionality for filtering manufacturers.
        [ObservableProperty]
        private List<Manufacturer> filteredManufacturersList = new();
        public void FilterManufacturers(string query)
        {
            IEnumerable<Manufacturer> itemsToDisplay = string.IsNullOrWhiteSpace(query)
                ? Manufacturers.ToList()
                : Manufacturers.Where(m => m.ManufacturerName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            filteredManufacturersList.Clear();
            foreach (Manufacturer m in itemsToDisplay)
            {
                filteredManufacturersList.Add(m);
            }
        }
    }
}
