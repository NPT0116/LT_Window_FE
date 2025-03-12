using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Manufacturer : BaseEntity
    {
        public string ManufacturerName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
