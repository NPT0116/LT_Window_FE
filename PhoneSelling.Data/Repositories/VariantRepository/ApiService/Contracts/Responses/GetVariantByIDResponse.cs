using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Common.Contracts.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;

namespace PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Responses
{
    public class GetVariantByIDResponse
    {
        public VariantDto data { get; set; }
        public bool succeeded { get; set; }
        public List<string>? errors { get; set; }
        public string? message { get; set; }
    }
}
