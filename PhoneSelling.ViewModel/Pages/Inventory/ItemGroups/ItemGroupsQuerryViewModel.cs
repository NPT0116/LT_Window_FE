using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Requests;

namespace PhoneSelling.ViewModel.Pages.Inventory.ItemGroups
{
    public partial class ItemGroupsQuerryViewModel : PaginationQueryViewModel<ItemGroup, ItemGroupsPaginationQuery>
    {
        public ItemGroupsQuerryViewModel(Func<ItemGroupsPaginationQuery, Task<PaginationResult<ItemGroup>>> fetchDataFunc) : base(fetchDataFunc)
        {
        }
    }
}
