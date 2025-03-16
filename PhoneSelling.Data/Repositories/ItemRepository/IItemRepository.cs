using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task CreateFullItem(CreateFullItemDto item);
        Task AddItemToItemGroup(Guid itemGroupId);
        Task<List<Item>> GetItemsBelongsToItemGroup(Guid itemGroupId);
        Task<Item> UpdateItem(Item item);
    }
}
