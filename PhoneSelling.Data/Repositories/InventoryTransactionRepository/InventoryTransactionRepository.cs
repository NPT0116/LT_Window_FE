using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Mapper;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using PhoneSelling.DependencyInjection;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository
{
    public partial class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly IInventoryTransactionApiService _apiService;
        public InventoryTransactionRepository()
        {
            _apiService = DIContainer.GetKeyedSingleton<IInventoryTransactionApiService>();
        }

        public async Task<List<HistoryTransaction>> GetTransactionHistoryByVariantID(Guid variantID)
        {
            var response = await _apiService.GetTransactionHistoryByVariantID(variantID);
            var transactions = response.data.Select(dto => new HistoryTransaction
            {
                TransactionID = Guid.Parse(dto.transactionID),
                VariantID = Guid.Parse(dto.variantID),
                TransactionType = dto.transactionType,
                Quantity = dto.quantity,
                TransactionDate = DateTime.Parse(dto.transactionDate, CultureInfo.InvariantCulture),
                InvoiceDetailID = string.IsNullOrWhiteSpace(dto.invoiceDetailID) ? (Guid?)null : Guid.Parse(dto.invoiceDetailID)
            }).ToList();

            return transactions;
        }

        public async Task CreateInboundTransaction(CreateInboundTransactionDto dto)
        {
            var request = dto.ToRequest();
            await _apiService.CreateInboundInventoryTransaction(request);
        }

        public async Task CreateInboundTransaction(List<CreateInboundTransactionDto> dto)
        {
            var items = dto.Select(x => x.ToRequest()).ToList();
            var request = new CreateMultipleInboundTransactionRequest
            {
                list = items
            };
            await _apiService.CreateInboundInventoryTransaction(request);
        }
    }
}
