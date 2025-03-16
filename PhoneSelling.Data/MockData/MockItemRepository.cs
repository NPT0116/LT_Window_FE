using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;

namespace PhoneSelling.Data.Repositories.ItemRepository
{
    public class MockItemRepository : IItemRepository
    {
        public Task AddItemToItemGroup(Guid itemGroupId)
        {
            throw new NotImplementedException();
        }

        public Task CreateFullItem(CreateFullItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetAll()
        {
            // Retrieve all iPhone groups using a combined function.
            var groups = MockPhoneData.CreateAllPhoneGroups();
            // Flatten the groups into a single collection of items.
            IEnumerable<Item> allItems = groups.SelectMany(group => group.Items);
            return Task.FromResult(allItems);
        }

        public Task<Item?> GetById(Guid id)
        {
            // Retrieve all groups and flatten them into one collection,
            // then search for the item with the matching id.
            var groups = MockPhoneData.CreateAllPhoneGroups();
            Item? item = groups.SelectMany(group => group.Items)
                               .FirstOrDefault(i => i.Id == id);
            return Task.FromResult(item);
        }

        public Task<List<Item>> GetItemsBelongsToItemGroup(Guid itemGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
