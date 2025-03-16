using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Responses;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.ApiService
{
    public class ItemApiService : BaseApiService, IItemApiService
    {
        public ItemApiService()
        {
            
        }

        protected override string Prefix { get; } = "Item";

        public async Task<bool> AddItemToItemGroup(string id)
        {
            try
            {
                var request = new
                {
                    itemGroupId = id
                };
                var response = await _httpClient.PostAsJsonAsync(ApiUrl + $"/{id}/AddToItemGroup", request);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating new: {ex.Message}");
                return false;
            }
        }

        public async Task CreateFullItem(CreateFullItemRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ApiUrl + "/CreateFullItem", request);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating new: {ex.Message}");
                //return null; // Return empty list on failure
            }
        }

        public async Task<ItemListResponse?> GetAllItems()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ItemListResponse>(ApiUrl);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching items: {ex.Message}");
                return null; // Return empty list on failure
            }
        }

        public async Task<SingleItemRepsonse?> GetItemById(string id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SingleItemRepsonse>(ApiUrl + $"/{id}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching item with id {id}: {ex.Message}");
                return null; // Return empty list on failure
            }
        }

        public async Task<ItemListResponse?> GetItemsInItemGroup(string itemGroupId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ItemListResponse>(ApiUrl + $"/ItemGroup/{itemGroupId}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching items in group id {itemGroupId}: {ex.Message}");
                return null; // Return empty list on failure
            }
        }

        public async Task UpdateItem(ItemDto item)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync<ItemDto>(ApiUrl + $"/{item.itemId}", item);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching items in group id {item.itemId}: {ex.Message}");

            }
        }
    }
}
