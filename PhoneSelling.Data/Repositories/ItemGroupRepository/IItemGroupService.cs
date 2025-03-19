using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository
{
    public interface IItemGroupService
    {
        Task<List<ItemGroup>> GetItemGroupsAsync();
        Task<CreateItemGroupResponse> CreateItemGroupAsync(CreateItemGroupRequest request);
    }
}
