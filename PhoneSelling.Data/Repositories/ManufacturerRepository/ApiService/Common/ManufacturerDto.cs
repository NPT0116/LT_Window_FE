using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService.Common
{
    public class ManufacturerDto
    {
        public Guid ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerDescription { get; set; }
    }
}
