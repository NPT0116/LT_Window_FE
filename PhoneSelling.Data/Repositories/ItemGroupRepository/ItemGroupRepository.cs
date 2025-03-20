using Amazon.Runtime;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Common.Utils;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository
{
    class ItemGroupRepository : IItemGroupRepository
    {
        private readonly IItemGroupApiService _itemGroupApiService;
        public ItemGroupRepository() { 
             _itemGroupApiService = DIContainer.GetKeyedSingleton<IItemGroupApiService>();
        }
        public async Task<ItemGroup> CreateItemGroupAsync(CreateItemGroupRequest request)
        {
            var response = await _itemGroupApiService.CreateItemGroupAsync(request);
            var validResponse = ApiResponseUtil.EnsureSuccess(response);
            var itemGroup = new ItemGroup
            {
                Id = validResponse.Data.ItemGroupID,
                ItemGroupName = validResponse.Data.ItemGroupName,
            };
            return itemGroup;
        }
        public async Task<PaginationResult<ItemGroup>> GetItemGroupsAsync(ItemGroupQueryParameter query)
        {
            var response = await _itemGroupApiService.GetItemGroupsAsync(query);
            
            var result = new PaginationResult<ItemGroup>
            {
                Data = response.data.Select(itemGroup => new ItemGroup
                {
                    Id = itemGroup.ItemGroupID,
                    ItemGroupName = itemGroup.ItemGroupName,
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
