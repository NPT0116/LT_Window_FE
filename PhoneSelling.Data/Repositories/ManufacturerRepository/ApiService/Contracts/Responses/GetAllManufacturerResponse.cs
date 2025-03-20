using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Common.Contracts.Responses;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Common;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Responses
{
    public class GetAllManufacturerResponse : PaginationApiResponse<ManufacturerDto>
    {
    }
}
