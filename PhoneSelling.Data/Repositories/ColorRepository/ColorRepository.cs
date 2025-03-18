using PhoneSelling.Data.Common.Utils;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ColorRepository.ApiService;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ColorRepository
{
    public class ColorRepository : IColorRepository
    {
        private readonly IColorApiService _colorApiService;
        public ColorRepository()
        {
            _colorApiService = DIContainer.GetKeyedSingleton<IColorApiService>();
        }
        public async Task<Color> GetByIdAsync(Guid id)
        {
            // Gọi API và await kết quả trả về dạng GetColorByIdResponse
            var response = await _colorApiService.GetByIdAsync(id);

            var validResponse = ApiResponseUtil.EnsureSuccess(response);

            var dto = validResponse.Data;
            var color = new Color
            {
                Id = Guid.Parse(dto.colorID),
                Name = dto.name,
                ItemId = Guid.Parse(dto.itemID),
                UrlImage = dto.urlImage
            };

            return color;
        }

        public async Task<string> GetImageByVariantIdAsync(Guid variantId)
        {
            var response = await _colorApiService.GetImageByVariantIdAsync(variantId);
            var validResponse = ApiResponseUtil.EnsureSuccess(response);
            return validResponse.Data;
        }

        public async Task<Color> UpdateAsync(Guid id, Color colorDto)
        {
            var response = await _colorApiService.UpdateAsync(id, colorDto);
            var validResponse = ApiResponseUtil.EnsureSuccess(response);
            var dto = validResponse.Data;
            var color = new Color
            {
                Id = Guid.Parse(dto.colorID),
                Name = dto.name,
                ItemId = Guid.Parse(dto.itemID),
                UrlImage = dto.urlImage
            };
            return color;

        }


    }
}
