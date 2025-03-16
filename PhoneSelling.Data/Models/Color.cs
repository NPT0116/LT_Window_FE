using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Color : BaseEntity
    {
        public string Storage {  get; set; }
        public int CostPrice { get; set; }
        public int SellingPrice { get; set; }
        public int StockQuantity { get; set; }

        // FK
        public Guid VariantId { get; set; }
        public Guid ItemId { get; set; }
    }
}
