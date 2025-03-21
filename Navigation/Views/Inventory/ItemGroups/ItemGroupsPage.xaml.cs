using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.ViewModel.Pages.Inventory.ItemGroups;
using PhoneSelling.Data.Models;
using Microsoft.UI.Xaml.Controls.Primitives;
using PhoneSelling.ViewModel.Pages.Inventory.Variants;

namespace Navigation.Views.Inventory.ItemGroups
{
    public sealed partial class ItemGroupsPage : Page
    {
        public ItemGroupsPageViewModel ViewModel { get; set; }
        public ItemGroupsPage()
        {
            this.InitializeComponent();
            ViewModel = new ItemGroupsPageViewModel();
            this.DataContext = ViewModel;
        }
        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (sender is ListView lv && lv.SelectedItem is ItemGroup group)
            {
                if (this.DataContext is ItemGroupsPageViewModel viewModel)
                {
                    MainPivot.SelectedIndex = 1;
                    viewModel.SelectedItemGroup = group;
                    viewModel.SelectedItem = group.Items.FirstOrDefault();
                }
            }
        }
        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView lv && lv.SelectedItem is Item item)
            {
                if (this.DataContext is ItemGroupsPageViewModel viewModel)
                {
                    MainPivot.SelectedIndex = 2;
                    viewModel.SelectedItem = item;
                }
            }
        }
        private void VariantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("You pick a variant");
        }
    }
}
