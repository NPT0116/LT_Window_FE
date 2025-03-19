using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.Data.Models;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;

using PhoneSelling.ViewModel.Pages.Items;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;
using PhoneSelling.Data.Repositories.ColorRepository.ApiService.Common;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository;
using System.Text.Json.Serialization;


namespace PhoneSelling.ViewModel.Pages.Inventory.CreateItemPages
{
    public partial class CreateItemPageViewModel:BasePageViewModel
    {
        [ObservableProperty] private string itemName;
        [ObservableProperty] private string? description;
        [ObservableProperty] private string? picture;
        [ObservableProperty] private DateTime? releaseDate = DateTime.UtcNow;
        [ObservableProperty] private ObservableCollection<Variant> variants;
        [ObservableProperty] private Guid itemGroupId;
        [ObservableProperty] private Guid manufacturerId;

        private readonly IItemGroupService _itemGroupService;

        // Handle selected
        private ItemGroup _selectedItemGroup { get; set; }
        public ItemGroup SelectedItemGroup
        {
            get { return _selectedItemGroup; }
            set { 
                if (_selectedItemGroup != value)
                {
                    _selectedItemGroup = value;
                    OnPropertyChanged(nameof(SelectedItemGroup));
                }
            }
        }
        private Manufacturer _selectedManufacturer { get; set; }
        public Manufacturer SelectedManufacturer
        {
            get { return _selectedManufacturer; }
            set
            {
                if (_selectedManufacturer != value)
                {
                    _selectedManufacturer = value;
                    OnPropertyChanged(nameof(SelectedManufacturer));
                } 
            }
        }

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
        // Mock data
        [ObservableProperty] private List<ItemGroup> itemGroups = new();

        public async Task LoadItemGroupsAsync()
        {
            var groups = await _itemGroupService.GetItemGroupsAsync();
            if (groups != null)
            {
                ItemGroups = groups;
            }
        }
        [ObservableProperty]
        private List<Manufacturer> manufacturers = new()
        {
            new Manufacturer {ManufacturerName="Apple", Id=Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")},
        };


        private readonly IItemRepository _itemRepository;
        public CreateItemPageViewModel()
        {
            itemName = string.Empty;
            description = string.Empty;
            picture = string.Empty;
            _newStorage = string.Empty;
            _newColor = string.Empty;
            variants = new ObservableCollection<Variant>();
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            _itemGroupService = DIContainer.GetKeyedSingleton<IItemGroupService>();
            _ = LoadItemGroupsAsync();
        }


        // Relay command
        //[RelayCommand]
        //private void CreateItem()
        //{
        //    Debug.WriteLine("Create Item With those information:");
        //    //Debug.WriteLine(SelectedItemGroup?.ItemGroupName);
        //    //Debug.WriteLine(itemName);
        //    //Debug.WriteLine(description);
        //    //Debug.WriteLine(picture);
        //    //Debug.WriteLine(releaseDate);
        //    //Debug.WriteLine(itemGroupId);
        //    //Debug.WriteLine(manufacturerId);
        //    foreach(Variant variant in variants)
        //    {
        //        Debug.WriteLine(variant.Storage);
        //        Debug.WriteLine(variant.StockQuantity);

        //        Debug.WriteLine(variant.CostPrice);

        //        Debug.WriteLine(variant.SellingPrice);
        //    }
        //}
        [RelayCommand]
        private void CreateVariant()
        {
            variants.Add(
                new Variant { Storage = "0", Color = new Color { Name = "Black" }, CostPrice = 0, SellingPrice = 0, StockQuantity = 0 }
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
            if (!string.IsNullOrEmpty(NewStorage) &&!StorageList.Contains(NewStorage))
            {
                StorageList.Add(NewStorage);
                UpdateVariants();
            }
        }
        [RelayCommand]
        private void AddNewColor()
        {
            if (!string.IsNullOrEmpty(NewColor) && !StorageList.Contains(NewColor))
            {
                ColorList.Add(NewColor);
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
        private void UpdateVariants()
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
                itemGroupID = SelectedItemGroup?.Id.ToString(),
                itemName = itemName,
                description = description,
                picture = Picture,
                releaseDate = releaseDate?.ToString("o"),
                manufacturerID = SelectedManufacturer?.Id.ToString()
            };


            Debug.WriteLine($"ItemDto mapped: {itemDto.itemName}, ReleaseDate: {itemDto.releaseDate}");

            // Map the ColorList (strings) to ColorDto objects.
            var colorDtos = ColorList.Select((c, index) => new Data.Repositories.ItemRepository.ApiService.Contracts.Requests.ColorDto
            {
                tempId = index + 1,
                name = c,
                urlImage = "", // You can update this if you have a URL.
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
            Debug.WriteLine("SelectedManufacturer ID: " + SelectedManufacturer?.Id);
            Debug.WriteLine("Item saved successfully.");
        }


    }
}
