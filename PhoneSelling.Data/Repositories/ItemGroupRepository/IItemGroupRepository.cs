using Amazon.Runtime;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository
{
    public interface IItemGroupRepository
    {
        Task<PaginationResult<ItemGroup>> GetItemGroupsAsync(ItemGroupQueryParameter query);
        Task<ItemGroup> CreateItemGroupAsync(CreateItemGroupRequest request);
    }
}
