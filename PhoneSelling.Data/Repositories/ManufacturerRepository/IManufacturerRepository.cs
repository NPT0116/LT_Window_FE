using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Requests;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository
{
    public interface IManufacturerRepository
    {
        Task<PaginationResult<Manufacturer>> GetManufacturersAsync(ManufacturerQueryParameter query);
        Task<Manufacturer> CreateManufacturerAsync(CreateManufacturerRequest query);
    }
}
