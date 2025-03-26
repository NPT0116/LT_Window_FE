using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService
{
    public class InventoryTransactionApiService : BaseApiService, IInventoryTransactionApiService
    {
        protected override string Prefix => "InventoryTransaction";

        public async Task<GetTransactionHistoryByVariantIDResponse> GetTransactionHistoryByVariantID(Guid variantId)
        {
            var endpoint = $"{ApiUrl}/{variantId.ToString()}";
            var response = await _httpClient.GetFromJsonAsync<GetTransactionHistoryByVariantIDResponse>(endpoint);
            return response ?? new GetTransactionHistoryByVariantIDResponse();

        }
        public async Task CreateInboundInventoryTransaction(CreateInboundTransactionRequest request)
        {
            var endpoint = ApiUrl + "/inbound";
            await _httpClient.PostAsJsonAsync<CreateInboundTransactionRequest>(ApiUrl, request);
        }

        public async Task<CreateMultipleInboundTransactionResponse> CreateInboundInventoryTransaction(CreateMultipleInboundTransactionRequest request)
        {
            var endpoint = ApiUrl + "/inbound/list";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, request);
            var _response = await response.Content.ReadFromJsonAsync<CreateMultipleInboundTransactionResponse>();
            return _response;
        }
    }
}
