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
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml;

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
        // Group Handle
        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (sender is ListView lv && lv.SelectedItem is ItemGroup group)
            {
                if (this.DataContext is ItemGroupsPageViewModel viewModel)
                {
                    viewModel.SelectedItemGroup = group;
                    viewModel.SelectedItem = group.Items.FirstOrDefault();
                }
            }
        }
        private void GroupItem_Tapped(object sender, TappedRoutedEventArgs args)
        {
            var element = sender as FrameworkElement;
            if (element?.DataContext is ItemGroup tappedGroup)
            {
                MainPivot.SelectedIndex = 1;
            }
        }
        // Item Handle
        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView lv && lv.SelectedItem is Item item)
            {
                if (this.DataContext is ItemGroupsPageViewModel viewModel)
                {
                    viewModel.SelectedItem = item;
                }
            }
        }
        private void Item_Tapped(object sender, TappedRoutedEventArgs a)
        {
            var element = sender as FrameworkElement;
            if (element?.DataContext is Item item)
            {
                MainPivot.SelectedIndex = 2;
            }
        }
        private void VariantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("You pick a variant");
        }
    }
}
