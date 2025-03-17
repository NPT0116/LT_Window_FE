using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Constants;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Pages.Common;
using PhoneSelling.ViewModel.Pages.Items;

public partial class ItemViewModel : BasePageViewModel
{
    [ObservableProperty] private Item item = new();
    [ObservableProperty] private ObservableCollection<string> selectedStorages = new();
    [ObservableProperty] public ObservableCollection<TempColor> colors = new();
    [ObservableProperty] public ObservableCollection<TempVariant> variants = new();
    private readonly IItemRepository _itemRepository;
    public List<string> Storages => StorageCapacity.SupportedCapacities.Select(sc => sc.Value).ToList();

    public ItemViewModel()
    {
        Item = new Item();
        SelectedStorages = new ObservableCollection<string>();
        Colors = new();
        Variants = new();
        _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
    }

    
    public IEnumerable<TempVariant> GetVariantsForColor(int colorTempId)
    {
        return Variants.Where(v => v.ColorTempId == colorTempId);
    }

    // Command to add a new color

    public void AddColor((string colorName, string colorUrl) colorData)
    {
        if (!string.IsNullOrWhiteSpace(colorData.colorName))
        {
            var newColor = new TempColor()
            {
               Name = colorData.colorName,
                TempId = TempColor.CURRENT_TEMP_ID++,
                UrlImage = colorData.colorUrl
            };

            Colors.Add(newColor);
            Debug.WriteLine($"🟢 Added color: {newColor.Name} (Total: {Colors.Count})");

            foreach (var storage in SelectedStorages)
            {
                var variant = new TempVariant()
                {
                    Storage = storage,
                    CostPrice = 0,
                    SellingPrice = 0,
                    StockQuantity = 0,
                    ColorTempId = newColor.TempId
                };
                Variants.Add(variant);
            }

            Debug.WriteLine($"🟢 Variants updated (Total: {Variants.Count})");
        }
    }


    // Command to remove a color
    [RelayCommand]
    private void RemoveColor(TempColor color)
    {
        var colorToRemove = Colors.FirstOrDefault(c => c.TempId == color.TempId);
        if(colorToRemove != null)
        Colors.Remove(colorToRemove);

        var variantsToRemove = Variants.Where(v => v.ColorTempId == color.TempId).ToList();
        foreach(var variant in variantsToRemove)
        {
            variantsToRemove.Remove(variant);
        }
    }

    // Command to save Item + Variants
    [RelayCommand]
    private async Task SaveItem()
    {
        // Ensure at least one storage and one color is selected
        if (SelectedStorages.Count == 0 || Colors.Count == 0)
        {
            // Show error message to user (implementation depends on UI framework)
            return;
        }

        var createFullItemDto = new CreateFullItemDto
        {
            Item = new PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common.ItemDto
            {
                itemGroupID = null,
                itemName = Item.ItemName,
                description = Item.Description,
                picture = Item.Picture,
                releaseDate = Item.ReleaseDate.ToString(),
                manufacturerID = null
            },
            Colors = Colors.Select(c => new PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests.ColorDto
            {
                tempId = c.TempId,
                name = c.Name,
                urlImage = c.UrlImage,
                colorId = c.ColorId.ToString()
            }).ToList()
        };

        await _itemRepository.CreateFullItem(createFullItemDto);
    }

    [RelayCommand]
    private void AddCapacity(string capacity)
    {
        selectedStorages.Add(capacity);
    }
}
