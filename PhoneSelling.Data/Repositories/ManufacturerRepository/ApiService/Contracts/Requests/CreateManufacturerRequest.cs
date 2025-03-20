using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Contracts.Requests
{
    public class CreateManufacturerRequest
    {
        public string ManufacturerName { get; set; }
        public string ManufacturerDescription { get; set; }

    }
}
