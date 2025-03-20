using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService
{
    public class InventoryTransactionApiService : BaseApiService, IInventoryTransactionApiService
    {
        protected override string Prefix => "InventoryTransaction";

        public async Task CreateInboundInventoryTransaction(CreateInboundTransactionRequest request)
        {
            var endpoint = ApiUrl + "/inbound";
            await _httpClient.PostAsJsonAsync<CreateInboundTransactionRequest>(ApiUrl, request);
        }
    }
}
