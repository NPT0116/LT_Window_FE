using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Inventory : BaseEntity
    {
        public int StockQuantity { get; set; }
        public DateTime LastUpdated { get; set; }

        // FK
        public Guid VariantId { get; set; }
    }
}
