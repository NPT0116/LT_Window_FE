using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService
{
    public interface IItemGroupApiService
    {
        Task<GetAllItemGroupResponse> GetItemGroupsAsync(ItemGroupQueryParameter itemGroupQueryParameter);
        Task<CreateItemGroupResponse> CreateItemGroupAsync(CreateItemGroupRequest createItemGroupRequest);
    }
}
