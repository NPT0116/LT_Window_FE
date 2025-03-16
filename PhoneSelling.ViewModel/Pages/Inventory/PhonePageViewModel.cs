using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using PhoneSelling.ViewModel.Helpers;

namespace PhoneSelling.ViewModel.Pages.Inventory
{
    public class PhonePageViewModel : BasePageViewModel
    {
        private ObservableCollection<ItemGroup> _seriesGroups;
        public ObservableCollection<ItemGroup> SeriesGroups
        {
            get => _seriesGroups;
            set
            {
                _seriesGroups = value;
                OnPropertyChanged(nameof(SeriesGroups));
            }
        }

        private ObservableCollection<Item> _allItems;
        public ObservableCollection<Item> AllItems
        {
            get => _allItems;
            set
            {
                _allItems = value;
                OnPropertyChanged(nameof(AllItems));
            }
        }

        private ObservableCollection<Variant> _allVariants;
        public ObservableCollection<Variant> AllVariants
        {
            get => _allVariants;
            set
            {
                _allVariants = value;
                OnPropertyChanged(nameof(AllVariants));
            }
        }

        // NEW: A helper collection for displaying variant info with parent name
        private ObservableCollection<VariantDisplay> _allVariantDisplays;
        public ObservableCollection<VariantDisplay> AllVariantDisplays
        {
            get => _allVariantDisplays;
            set
            {
                _allVariantDisplays = value;
                OnPropertyChanged(nameof(AllVariantDisplays));
            }
        }

        private ItemGroup _selectedGroup;
        public ItemGroup SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (_selectedGroup != value)
                {
                    _selectedGroup = value;
                    OnPropertyChanged(nameof(SelectedGroup));
                    SelectedItem = _selectedGroup?.Items.FirstOrDefault();
                }
            }
        }

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

        private readonly IItemRepository _itemRepository;

        public PhonePageViewModel()
        {
            Debug.WriteLine("Phone Page ViewModel Creation!");
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            LoadSeriesGroups();
        }

        private void LoadSeriesGroups()
        {
            var groups = MockPhoneData.CreateAllPhoneGroups();
            SeriesGroups = new ObservableCollection<ItemGroup>(groups);

            // Flatten items
            var items = groups.SelectMany(g => g.Items).ToList();
            AllItems = new ObservableCollection<Item>(items);

            // Flatten variants
            var variants = items.SelectMany(i => i.Variants).ToList();
            AllVariants = new ObservableCollection<Variant>(variants);

            // NEW: Build a list of VariantDisplay objects
            var variantDisplays = new List<VariantDisplay>();
            foreach (var group in groups)
            {
                foreach (var item in group.Items)
                {
                    foreach (var variant in item.Variants)
                    {
                        variantDisplays.Add(new VariantDisplay
                        {
                            ParentItemName = item.ItemName,
                            Variant = variant
                        });
                    }
                }
            }
            AllVariantDisplays = new ObservableCollection<VariantDisplay>(variantDisplays);

            // Set default selection if available
            if (SeriesGroups.Any())
            {
                SelectedGroup = SeriesGroups.First();
                if (SelectedGroup.Items.Any())
                {
                    SelectedItem = SelectedGroup.Items.First();
                }
            }
        }
    }
}
