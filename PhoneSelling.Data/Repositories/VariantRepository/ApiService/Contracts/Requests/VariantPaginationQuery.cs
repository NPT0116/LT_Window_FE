using PhoneSelling.Data.Common.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests
{
    public class VariantPaginationQuery : PaginationQuery
    {
        public string StorageFilter { get; set; } = string.Empty;
        public string? ManufacturerFilter { get; set; }
    }
}
