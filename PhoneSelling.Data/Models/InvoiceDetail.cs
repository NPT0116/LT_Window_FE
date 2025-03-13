using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class InvoiceDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public float Price { get; set; }
        public Variant Variant { get; set; } = new();

        // FK
        public Guid InvoiceId { get; set; }
        public Guid VariantId { get; set; }
    }
}
