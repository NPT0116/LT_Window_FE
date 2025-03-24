using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.VariantRepository.ApiService
{
    public interface IVariantApiService
    {
        Task<GetVariantByIDResponse> GetVariantById(Guid variantId);
        Task<GetAllVariantsResponse> GetAllVariants(VariantPaginationQuery query);

    }
}
