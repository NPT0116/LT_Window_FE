using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Pages.Inventory.Transaction.TransactionHistory;

namespace Navigation.Views.Inventory.Transaction
{
    public sealed partial class TransactionHistoryPage : Page
    {
        public TransactionHistoryPageViewModel ViewModel { get; set; }
        private IInventoryTransactionRepository _inventoryTransactionRepository;

        public TransactionHistoryPage()
        {
            this.InitializeComponent();
            ViewModel = new TransactionHistoryPageViewModel();
            this.DataContext = ViewModel;
            _inventoryTransactionRepository = DIContainer.GetKeyedSingleton<IInventoryTransactionRepository>();
        }
        private async void ItemSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                Debug.WriteLine("SearchBox Changed !");
                if (string.IsNullOrEmpty(sender.Text))
                {
                    Debug.WriteLine("Item is unselected !");
                    ViewModel.SelectedVariant = new Variant();
                    ViewModel.TransactionHistory = null;
                }
                var variants = await App.SearchVariants(sender.Text);
                if (variants != null)
                {
                    sender.ItemsSource = variants.OrderBy(v => v.Item.ItemName);
                } 
            }
        }

        private void ItemSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Variant selectedItem)
            {
                Debug.WriteLine(selectedItem.Item.ItemName);
                sender.Text = $"{selectedItem.Item.ItemName} - {selectedItem.Color.Name} - {selectedItem.Storage}";
                ViewModel.SelectedVariant = selectedItem;
                Debug.WriteLine(selectedItem.VariantID);
                LoadTransactionHistory(selectedItem.VariantID);
            }
        }
        public async Task LoadTransactionHistory(Guid id)
        {
            ViewModel.TransactionHistory = await _inventoryTransactionRepository.GetTransactionHistoryByVariantID(id);
            if (ViewModel.TransactionHistory.Count() > 0) {
                Debug.WriteLine("Product has history");
                ViewModel.IsTransactionHistoryEmpty = false;
            } else
            {
                Debug.WriteLine("Product has no history");
                ViewModel.IsTransactionHistoryEmpty = true;
            }
        }

    }
}
