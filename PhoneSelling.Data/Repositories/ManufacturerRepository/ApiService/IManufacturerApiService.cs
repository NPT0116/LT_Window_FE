using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Responses;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService
{
    interface IManufacturerApiService
    {
        Task<GetAllManufacturerResponse> GetManufacturersAsync(ManufacturerQueryParameter manufacturerQueryParameter);
        Task<CreateManufacturerResponse> CreateManufacturerAsync(CreateManufacturerRequest createManufacturerRequest);
    }
}
