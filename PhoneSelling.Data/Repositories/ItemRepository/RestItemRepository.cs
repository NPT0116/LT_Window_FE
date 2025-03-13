using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository
{
    public class RestItemRepository : IItemRepository
    {
        private readonly IItemApiService _itemApiService;
        public RestItemRepository()
        {
            _itemApiService = DIContainer.GetKeyedSingleton<IItemApiService>();
        }

        public Task CreateFullItem(CreateFullItemDto item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            var response = await _itemApiService.GetAllItems();
            if(response == null || response.Data.Count() == 0) return Enumerable.Empty<Item>();
            
            return response.Data.Select(dto => 
            {
                Guid.TryParse(dto.itemGroupId, out Guid itemGroupId);
                Guid.TryParse(dto.manufacturerId, out Guid manufacturerId);

                // Safely parse DateTime
                DateTime.TryParse(dto.releasedDate, out DateTime releaseDate);
                return new Item
                {
                    ItemGroupId = itemGroupId,
                    ManufacturerId = manufacturerId,
                    ItemName = dto.itemName,
                    Description = dto.Description,
                    Picture = dto.picture,
                    ReleasedDate = releaseDate
                };
            }).ToList();
        }

        public async Task<Item?> GetById(Guid id)
        {
            var response = await _itemApiService.GetItemById(id.ToString());
            if (response == null) return null;

            var dto = response.Data;
            return new Item
            {
                ItemGroupId = Guid.Parse(dto.itemGroupId),
                ManufacturerId = Guid.Parse(dto.manufacturerId),
                ItemName = dto.itemName,
                Description = dto.Description,
                Picture = dto.picture,
                ReleasedDate = DateTime.Parse(dto.releasedDate)
            };
        }
    }
}
