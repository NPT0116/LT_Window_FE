using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Common.Contracts.Requests;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Query
{
    public class ManufacturerQueryParameter : PaginationQuery
    {
        public string ManufacturerName { get; set; } = String.Empty;
        public string ManufacturerDescription { get; set; } = String.Empty;

    }
}