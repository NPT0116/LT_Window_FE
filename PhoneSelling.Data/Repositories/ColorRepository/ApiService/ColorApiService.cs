using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.ColorRepository.ApiService.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ColorRepository.ApiService
{
    public class ColorApiService : BaseApiService,IColorApiService
    {


        public ColorApiService() {
        }

        protected override string Prefix { get; } = "Color";

        public async Task<GetColorByIdResponse> GetByIdAsync(Guid id)
        {
            var response =  await _httpClient.GetFromJsonAsync<GetColorByIdResponse>(ApiUrl + $"/{id}");
            return response;
        }

        public async Task<GetImageByVariantIdResponse> GetImageByVariantIdAsync(Guid variantId)
        {
            var response = await _httpClient.GetFromJsonAsync<GetImageByVariantIdResponse>(ApiUrl + $"/{variantId}");
            return response;
        }

        public async Task<UpdateColorResponse> UpdateAsync(Guid id, Color colorDto)
        {
            var response = await _httpClient.PutAsJsonAsync(ApiUrl + $"/{id}", colorDto);
            return await response.Content.ReadFromJsonAsync<UpdateColorResponse>();
        }
    }
}
