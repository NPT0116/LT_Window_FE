using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;

namespace PhoneSelling.ViewModel.Pages.Inventory.Transaction.TransactionHistory
{
    public partial class TransactionHistoryPageViewModel : BasePageViewModel
    {
        private IVariantRepository _variantRepository { get; set; }
        private IInventoryTransactionRepository _inventoryTransactionRepository { get; set; }
        [ObservableProperty] private Variant selectedVariant = new Variant();
        [ObservableProperty] private List<HistoryTransaction> transactionHistory;

        public TransactionHistoryPageViewModel()
        {
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();
            _inventoryTransactionRepository = DIContainer.GetKeyedSingleton<IInventoryTransactionRepository>();

        }
        public async Task<List<Variant>> SearchVariants(string text)
        {
            var query = new VariantPaginationQuery
            {
                PageNumber = 1,
                PageSize = 5,
                SearchKey = text
            };
            var paginationResult = await _variantRepository.GetAllVariants(query);
            return paginationResult.Data;
        }
        //public async Task LoadTransactionHistory(Guid id)
        //{
        //    transactionHistory = await _inventoryTransactionRepository.GetTransactionHistoryByVariantID(id);
        //}
    }
}
