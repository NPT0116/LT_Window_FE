using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
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
    public class ItemApiService : BaseApiService,IItemApiService
    {
        public ItemApiService()
        {
            
        }

        protected override string Prefix { get; } = "Item";

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
    }
}
