using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Responses;
using System;
using System.Collections.Generic;
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
            var response = await _httpClient.GetFromJsonAsync<GetAllVariantsResponse>(ApiUrl + "/GetAll");
            return response;
        }
    }
}
