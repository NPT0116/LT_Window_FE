using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.ViewModel.Base;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;
using PhoneSelling.DependencyInjection;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.ItemGroupRepository;
using System.Text.Json.Serialization;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ManufacturerRepository;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Query;
using PhoneSelling.ViewModel.Pages.Items;


namespace PhoneSelling.ViewModel.Pages.Inventory.CreateItemPages
{

    public partial class CreateItemPageViewModel : BasePageViewModel
    {
        [ObservableProperty] private string itemName;
        [ObservableProperty] private string? description;
        [ObservableProperty] private string? picture;
        [ObservableProperty] private DateTime? releaseDate = DateTime.UtcNow;
        [ObservableProperty] private ObservableCollection<Variant> variants;
        [ObservableProperty] private Guid itemGroupId;
        [ObservableProperty] private Guid manufacturerId;
        // Handle selected
        [ObservableProperty] private ItemGroup selectedItemGroup;
        [ObservableProperty] private Manufacturer selectedManufacturer;
        // Manually add Storage and Color
        // Storage
        public ObservableCollection<string> StorageList { get; } = new ObservableCollection<string>();
        private string _newStorage { set; get; }
        public string NewStorage
        {
            get { return _newStorage; }
            set
            {
                if (_newStorage != value)
                {
                    _newStorage = value;
                    if (!StorageList.Contains(value))
                    {
                        OnPropertyChanged(nameof(NewStorage));
                    }
                }
            }
        }
        // Color
        public ObservableCollection<string> ColorList { get; } = new ObservableCollection<string>();
        public ObservableCollection<TempColor> ColorObjectList { get; } = new ObservableCollection<TempColor>();
        private string _newColor { set; get; }
        public string NewColor
        {
            get { return _newColor; }
            set
            {
                if (_newColor != value)
                {
                    _newColor = value;
                    if (!ColorList.Contains(value))
                    {
                        OnPropertyChanged(nameof(NewColor));
                    }
                }
            }
        }
        // Fetch data for item group and manufacturer
        [ObservableProperty] private List<ItemGroup> itemGroups = new();
        public async Task LoadItemGroupsAsync()
        {
            var groups = await _itemGroupRepository.GetItemGroupsAsync(new ItemGroupQueryParameter());
            if (groups != null)
            {
                // Clear the existing collection and add the fetched groups.
                itemGroups.Clear();
                foreach (var group in groups.Data)
                {
                    itemGroups.Add(group);
                }
            }
        }
        [ObservableProperty] private List<Manufacturer> manufacturers = new();
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
            itemName = string.Empty;
            description = string.Empty;
            picture = string.Empty;
            _newStorage = string.Empty;
            _newColor = string.Empty;
            variants = new ObservableCollection<Variant>();

            // Create Item to Save
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            // Display and create Item Group
            _itemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>();
            _ = LoadItemGroupsAsync();
            // Display and create Manufacturer
            _manufacturerRepository = DIContainer.GetKeyedSingleton<IManufacturerRepository>();
            _ = LoadManufacturerAsync();
            //
            //filteredManufacturersList = manufacturers;
        }

        // Relay command
        [RelayCommand]
        private void CreateVariant()
        {
            variants.Add(
                new Variant { Storage = "0", Color = new Color { Name = ColorObjectList.FirstOrDefault().Name }, CostPrice = 0, SellingPrice = 0, StockQuantity = 0 }
            );
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

        // Update variants list
        public void UpdateVariants()
        {
            //variants.Clear();
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
            // Validate that at least one storage and one color exist.
            if (StorageList.Count == 0 || ColorList.Count == 0)
            {
                Debug.WriteLine("Error: At least one storage and one color must be entered.");
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
                picture = Picture,
                releaseDate = releaseDate?.ToString("o"),
                manufacturerID = SelectedManufacturer?.Id.ToString()
            };


            Debug.WriteLine($"ItemDto mapped: {itemDto.itemName}, ReleaseDate: {itemDto.releaseDate}");

            // Map the ColorObjectList (strings) to ColorDto objects.
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
                // Match the variant's color with the color DTO by name.
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
        // Search Box
        [ObservableProperty] private List<Manufacturer> filteredManufacturersList = new();
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
