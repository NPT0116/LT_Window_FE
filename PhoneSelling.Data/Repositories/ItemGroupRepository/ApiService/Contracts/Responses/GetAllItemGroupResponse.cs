using Amazon.Runtime;
using PhoneSelling.Data.Common.Contracts.Responses;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Responses
{
    public class GetAllItemGroupResponse : PaginationApiResponse<ItemGroupDto>
    {

    }
}
