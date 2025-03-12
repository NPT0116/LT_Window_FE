using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Variant : BaseEntity
    {
        public string Color { get; } = string.Empty;
        public int Storage { get; set; }
        public float CostPrice { get; set; }
        public float SellingPrice { get; set; }
        public int StockQuantity { get; set; }
        public Inventory Inventory { get; set; } = new();

        // FK
        public int ItemId { get; set; }
    }
}
