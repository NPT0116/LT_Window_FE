using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService
{
    class ItemGroupApiService : BaseApiService,IItemGroupApiService
    {
        protected override string Prefix { get; } = "ItemGroup";

        public async Task<CreateItemGroupResponse> CreateItemGroupAsync(CreateItemGroupRequest createItemGroupRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/Create", createItemGroupRequest);
            return await response.Content.ReadFromJsonAsync<CreateItemGroupResponse>();
        }

        public async Task<GetAllItemGroupResponse> GetItemGroupsAsync(ItemGroupQueryParameter itemGroupQueryParameter)
        {
            var queryParams = new List<string>
            {
                $"pageNumber={itemGroupQueryParameter.PageNumber}",
                $"pageSize={itemGroupQueryParameter.PageSize}"
            };
            if (!string.IsNullOrWhiteSpace(itemGroupQueryParameter.ItemGroupName))
                queryParams.Add($"search={Uri.EscapeDataString(itemGroupQueryParameter.ItemGroupName)}");
            string url = $"{ApiUrl}?{string.Join("&", queryParams)}";

            var response = await _httpClient.GetFromJsonAsync<GetAllItemGroupResponse>(url);
            return response;
        }
    }
}
