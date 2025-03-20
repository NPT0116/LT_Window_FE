using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Mapper;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using PhoneSelling.DependencyInjection;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly IInventoryTransactionApiService _apiService;
        public InventoryTransactionRepository()
        {
            _apiService = DIContainer.GetKeyedSingleton<IInventoryTransactionApiService>();
        }
        public async Task CreateInboundTransaction(CreateInboundTransactionDto dto)
        {
            var request = dto.ToRequest();
            await _apiService.CreateInboundInventoryTransaction(request);
        }
    }
}
