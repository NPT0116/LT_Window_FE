using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Common.Utils;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository
{
    class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IManufacturerApiService _manufacturerApiService;
        public ManufacturerRepository()
        {
            _manufacturerApiService = DIContainer.GetKeyedSingleton<IManufacturerApiService>();
        }
        public async Task<Manufacturer> CreateManufacturerAsync(CreateManufacturerRequest query)
        {
            var response = await _manufacturerApiService.CreateManufacturerAsync(query);
            var validResponse = ApiResponseUtil.EnsureSuccess(response);
            var manufacturer = new Manufacturer
            {
                Id = validResponse.Data.ManufacturerID,
                ManufacturerName = validResponse.Data.ManufacturerName,
                Description = validResponse.Data.ManufacturerDescription
            };
            return manufacturer;
        }

        public async Task<PaginationResult<Manufacturer>> GetManufacturersAsync(ManufacturerQueryParameter query)
        {
            var response = await _manufacturerApiService.GetManufacturersAsync(query);

            var result = new PaginationResult<Manufacturer>
            {
                Data = response.data.Select(manufacturer => new Manufacturer
                {
                    Id = manufacturer.ManufacturerID,
                    ManufacturerName = manufacturer.ManufacturerName,
                    Description = manufacturer.ManufacturerDescription,
                }).ToList(),
                PageNumber = response.pageNumber,
                PageSize = response.pageSize,
                TotalPages = response.totalPages,
                TotalRecords = response.totalRecords,

            };
            return result;
        }
    }
}
