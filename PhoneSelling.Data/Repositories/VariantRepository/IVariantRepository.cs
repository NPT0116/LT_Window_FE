using PhoneSelling.Data.Common.Internal.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Common.Contracts.Requests;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
namespace PhoneSelling.Data.Repositories.VariantRepository
{
    public  interface IVariantRepository
    {
        Task<PaginationResult<Variant>> GetAllVariants(VariantPaginationQuery query);
        Task<Variant> GetVariantById(Guid variantId);

    }
}
