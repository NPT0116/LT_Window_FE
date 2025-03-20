using PhoneSelling.Common;
using PhoneSelling.Data.Common.Contracts.Requests;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.VariantRepository.ApiService
{
    public class VariantApiService : BaseApiService, IVariantApiService
    {
        protected override string Prefix => "Variant";

        public async Task<GetAllVariantsResponse> GetAllVariants(VariantPaginationQuery query)
        {
            // Ensure query is not null
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            // Construct query parameters dynamically
            var queryParams = new List<string>
            {
                $"pageNumber={query.PageNumber}",
                $"pageSize={query.PageSize}"
            };

            if (!string.IsNullOrWhiteSpace(query.SearchKey))
                queryParams.Add($"search={Uri.EscapeDataString(query.SearchKey)}");

            if (!string.IsNullOrWhiteSpace(query.Sortby))
                queryParams.Add($"sortBy={query.Sortby}");

            if (query.SortDirection != null)
            {
                queryParams.Add($"sortDirection={EnumHelper.GetEnumMemberValue(query.SortDirection)}");
            }

            if (!string.IsNullOrWhiteSpace(query.StorageFilter))
                queryParams.Add($"storage={Uri.EscapeDataString(query.StorageFilter)}");

            if (!string.IsNullOrWhiteSpace(query.ManufacturerFilter))
                queryParams.Add($"manufacturer={query.ManufacturerFilter}");

            // Join query parameters
            string url = $"{ApiUrl}/GetAll?{string.Join("&", queryParams)}";
            Debug.WriteLine(url);

            // Fetch data from API
            var response = await _httpClient.GetFromJsonAsync<GetAllVariantsResponse>(url);

            return response ?? new GetAllVariantsResponse(); // Return empty response if null
        }
    }
}
