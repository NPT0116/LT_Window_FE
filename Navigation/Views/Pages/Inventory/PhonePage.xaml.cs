using System.Linq;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Pages.Inventory;

namespace Navigation.Views.Inventory
{
    public sealed partial class PhonePage : Page
    {
        private PhonePageViewModel _viewModel { get; set; }

        public PhonePage()
        {
            this.InitializeComponent();
            _viewModel = new PhonePageViewModel();
        }

        private void GroupsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ItemGroup selectedGroup)
            {
                _viewModel.SelectedGroup = selectedGroup;
                _viewModel.SelectedItem = selectedGroup.Items.FirstOrDefault();
            }
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Item selectedItem)
            {
                _viewModel.SelectedItem = selectedItem;
            }
        }

        private void VariantsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optionally handle variant selection.
        }
    }
}
