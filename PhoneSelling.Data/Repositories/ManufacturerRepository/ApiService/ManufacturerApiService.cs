using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Responses;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService
{
    class ManufacturerApiService : BaseApiService, IManufacturerApiService
    {
        protected override string Prefix { get; } = "Manufacturer";
        public async Task<CreateManufacturerResponse> CreateManufacturerAsync(CreateManufacturerRequest createManufacturerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}", createManufacturerRequest);
            return await response.Content.ReadFromJsonAsync<CreateManufacturerResponse>();
        }

        public async Task<GetAllManufacturerResponse> GetManufacturersAsync(ManufacturerQueryParameter manufacturerQueryParameter)
        {
            var queryParams = new List<string>
            {
                $"pageNumber={manufacturerQueryParameter.PageNumber}",
                $"pageSize={manufacturerQueryParameter.PageSize}"
            };
            if (!string.IsNullOrWhiteSpace(manufacturerQueryParameter.ManufacturerName))
                queryParams.Add($"search={Uri.EscapeDataString(manufacturerQueryParameter.ManufacturerName)}");
            string url = $"{ApiUrl}?{string.Join("&", queryParams)}";

            var response = await _httpClient.GetFromJsonAsync<GetAllManufacturerResponse>(url);

            return response;
        }
    }
}
