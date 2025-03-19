using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository
{
    public class ItemGroupService : IItemGroupService
    {
        private readonly HttpClient _client;

        public ItemGroupService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ItemGroup>> GetItemGroupsAsync()
        {
            try
            {
                // Call your API endpoint and deserialize into the ApiResponse wrapper.
                var response = await _client.GetFromJsonAsync<ApiResponse<List<ItemGroup>>>("http://localhost:5142/api/ItemGroup");
                if (response != null && response.Succeeded)
                {
                    return response.Data;
                }
                else
                {
                    Debug.WriteLine("Error: API response unsuccessful or null.");
                    return new List<ItemGroup>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching item groups: {ex.Message}");
                return new List<ItemGroup>();
            }
        }

        public async Task<CreateItemGroupResponse> CreateItemGroupAsync(CreateItemGroupRequest request)
        {
            try
            {
                // POST to your API endpoint for creating an item group.
                var response = await _client.PostAsJsonAsync("http://localhost:5142/api/ItemGroup/Create", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CreateItemGroupResponse>();
                    return result ?? new CreateItemGroupResponse();
                }
                else
                {
                    Debug.WriteLine("Error posting new item group: " + response.ReasonPhrase);
                    return new CreateItemGroupResponse();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error posting item group: {ex.Message}");
                return new CreateItemGroupResponse();
            }
        }
    }
}
