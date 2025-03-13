using PhoneSelling.Data.Adapters;
using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository
{
    public class RestItemRepository : IItemRepository
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:5142/api/Item";

        public RestItemRepository()
        {
            _httpClient = DIContainer.GetKeyedSingleton<HttpClient>();
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ListApiResponse<ItemListDto>>(ApiUrl);
                if (response?.Data == null) return new List<Item>();

                return ItemAdapter.ConvertToItems(response.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching items: {ex.Message}");
                return new List<Item>(); // Return empty list on failure
            }
        }

        public Task<Item?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
