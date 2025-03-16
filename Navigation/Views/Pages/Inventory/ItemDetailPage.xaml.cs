using System.Diagnostics;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Pages.Inventory;

namespace Navigation.Views.Inventory
{
    public sealed partial class ItemDetailPage : Page
    {
        private ItemDetailPageViewModel _viewModel;

        public ItemDetailPage()
        {
            this.InitializeComponent();
            _viewModel = new ItemDetailPageViewModel();
            this.DataContext = _viewModel;
        }

        // When a brand is selected from the Brands ListView.
        private void BrandsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ItemGroup selectedBrand)
            {
                _viewModel.SelectedBrand = selectedBrand;
                // Switch pivot to the Groups tab (index 1)
                MainPivot.SelectedIndex = 1;
            }
        }

        // When a subgroup is selected from the Groups ListView.
        private void GroupsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ItemsSubGroup selectedSubGroup)
            {
                _viewModel.SelectedItemsGroup = selectedSubGroup;
                // Switch pivot to the Items tab (index 2)
                MainPivot.SelectedIndex = 2;
            }
        }

        // When an item is selected from the Items ListView.
        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Item selectedItem)
            {
                _viewModel.SelectedItem = selectedItem;
                // Switch pivot to the Details tab (index 3)
                MainPivot.SelectedIndex = 3;
            }
        }
    }
}
