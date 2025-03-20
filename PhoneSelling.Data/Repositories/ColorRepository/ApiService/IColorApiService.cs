using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ColorRepository.ApiService.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ColorRepository.ApiService
{
    public interface IColorApiService
    {
        Task<GetColorByIdResponse> GetByIdAsync(Guid id);
        Task<UpdateColorResponse> UpdateAsync(Guid id, Color colorDto);
        Task<GetImageByVariantIdResponse> GetImageByVariantIdAsync(Guid variantId);
    }
}
