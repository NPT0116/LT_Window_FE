using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.ViewModel.Base;

namespace PhoneSelling.ViewModel.Pages.Inventory
{
    /// <summary>
    /// ViewModel for a four-level hierarchy:
    /// 1. Brands (top-level groups: iPhone, Samsung Galaxy, etc.)
    /// 2. Items Groups (subgroups within a brand)
    /// 3. Items (individual items within a subgroup)
    /// 4. Details (the selected item’s details)
    /// </summary>
    public class ItemDetailPageViewModel : BasePageViewModel
    {
        // Top-level collection of brands.
        public ObservableCollection<ItemGroup> Brands { get; set; }

        private ItemGroup _selectedBrand;
        /// <summary>
        /// The currently selected brand.
        /// When set, loads the subgroup items for that brand.
        /// </summary>
        public ItemGroup SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                if (_selectedBrand != value)
                {
                    _selectedBrand = value;
                    OnPropertyChanged(nameof(SelectedBrand));
                    LoadItemsGroups();
                }
            }
        }

        // Collection of subgroups (ItemsSubGroup) within the selected brand.
        private ObservableCollection<ItemsSubGroup> _itemsGroups;
        public ObservableCollection<ItemsSubGroup> ItemsGroups
        {
            get => _itemsGroups;
            set
            {
                if (_itemsGroups != value)
                {
                    _itemsGroups = value;
                    OnPropertyChanged(nameof(ItemsGroups));
                }
            }
        }

        private ItemsSubGroup _selectedItemsGroup;
        public ItemsSubGroup SelectedItemsGroup
        {
            get => _selectedItemsGroup;
            set
            {
                if (_selectedItemsGroup != value)
                {
                    _selectedItemsGroup = value;
                    OnPropertyChanged(nameof(SelectedItemsGroup));
                    // Update SelectedItem to first item in subgroup.
                    SelectedItem = _selectedItemsGroup?.Items.FirstOrDefault();
                }
            }
        }

        // The selected item for the details view.
        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        // For completeness, a flattened list of all items (if needed).
        public ObservableCollection<Item> AllItems { get; set; }
        public ItemDetailPageViewModel()
        {
            Debug.WriteLine("ItemDetailPageViewModel Creation!");
            // Load brands from mock data.
            List<ItemGroup> groups = MockPhoneData.CreateAllPhoneGroups();
            Brands = new ObservableCollection<ItemGroup>(groups);

            // Optionally, build AllItems (flattened) if needed.
            AllItems = new ObservableCollection<Item>(groups.SelectMany(g => g.Items));

            // Set default selected brand if available.
            if (Brands.Any())
            {
                SelectedBrand = Brands.First();
            }
        }

        /// <summary>
        /// Groups items within the SelectedBrand into subgroups.
        /// Here we group by the first two words of the ItemName.
        /// </summary>
        private void LoadItemsGroups()
        {
            if (SelectedBrand == null)
            {
                ItemsGroups = new ObservableCollection<ItemsSubGroup>();
                return;
            }

            // Group items within the selected brand.
            var groups = SelectedBrand.Items.GroupBy(item =>
            {
                var parts = item.ItemName.Split(' ');
                return parts.Length >= 2 ? string.Join(" ", parts.Take(2)) : item.ItemName;
            })
            .Select(g => new ItemsSubGroup
            {
                GroupName = g.Key,
                Items = new ObservableCollection<Item>(g.ToList())
            })
            .ToList();

            ItemsGroups = new ObservableCollection<ItemsSubGroup>(groups);
            
            Debug.WriteLine("New Items Group is Created");
            foreach(var subgroup in ItemsGroups)
            {
                Debug.WriteLine($"Subgroup: {subgroup.GroupName} (Items: {subgroup.Items.Count})");
            }

            // Check if the previously selected subgroup exists in the new brand.
            if (SelectedItemsGroup != null)
            {
                var matchingSubGroup = ItemsGroups.FirstOrDefault(sg => sg.GroupName == SelectedItemsGroup.GroupName);
                if (matchingSubGroup != null)
                {
                    SelectedItemsGroup = matchingSubGroup;
                    return;
                }
            }
            // Otherwise, default to the first subgroup.
            if (ItemsGroups.Any())
            {
                SelectedItemsGroup = ItemsGroups.First();
                Debug.WriteLine($"Default SelectedItemsGroup set to: {SelectedItemsGroup.GroupName}");
            }
        }
    }
}
