using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Common
{
    public class CustomerDto
    {
        public Guid customerID { get; set; }
        public string name { get; set; } = string.Empty;
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
    }
}
