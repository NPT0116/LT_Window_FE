using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.ItemRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.ApiService
{
    public interface IItemApiService
    {
        Task<ItemListResponse?> GetAllItems();
        Task<SingleItemRepsonse?> GetItemById(string id);
        Task CreateFullItem(CreateFullItemRequest request);
    }
}
