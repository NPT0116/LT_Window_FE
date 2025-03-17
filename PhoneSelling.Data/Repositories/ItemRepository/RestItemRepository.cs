using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests;
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

        public async Task AddItemToItemGroup(Guid itemGroupId)
        {
            await _itemApiService.AddItemToItemGroup(itemGroupId.ToString());
        }

        public async Task CreateFullItem(CreateFullItemDto item)
        {

            var request = new CreateFullItemRequest
            {
                item = item.Item,
                colors = item.Colors,
                variants = item.Variants
            };

            await _itemApiService.CreateFullItem(request);
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            var response = await _itemApiService.GetAllItems();
            if(response == null || response.Data.Count() == 0) return Enumerable.Empty<Item>();
            
            return response.Data.Select(dto => 
            {
                Guid.TryParse(dto.itemGroupID, out Guid itemGroupId);
                Guid.TryParse(dto.manufacturerID, out Guid manufacturerId);

                // Safely parse DateTime
                DateTime.TryParse(dto.releaseDate, out DateTime releaseDate);
                return new Item
                {
                    ItemGroupId = itemGroupId,
                    ManufacturerId = manufacturerId,
                    ItemName = dto.itemName,
                    Description = dto.description,
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
                ItemGroupId = Guid.Parse(dto.itemGroupID),
                ManufacturerId = Guid.Parse(dto.manufacturerID),
                ItemName = dto.itemName,
                Description = dto.description,
                Picture = dto.picture,
                ReleasedDate = DateTime.Parse(dto.releaseDate)
            };
        }

        public async Task<List<Item>> GetItemsBelongsToItemGroup(Guid itemGroupId)
        {
            var response = await _itemApiService.GetItemsInItemGroup(itemGroupId.ToString());
            if(response == null  || response.Data == null || response.Data.Count() == 0 ) return Enumerable.Empty<Item>().ToList();
            
            var dtos = response.Data;
            return dtos.Select(dto =>
            {
                var item = new Item
                {
                    ItemGroupId = Guid.Parse(dto.itemGroupID),
                    ManufacturerId = Guid.Parse(dto.manufacturerID),
                    ItemName = dto.itemName,
                    Description = dto.description,
                    Picture = dto.picture,
                    ReleasedDate = DateTime.Parse(dto.releaseDate)
                };
                return item;
            }).ToList();
        }

        public Task<Item> UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
