using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class InvoiceDetail : BaseEntity
    {
        public int Quantity { get; }
        public float Price { get; }
        public Variant Variant { get; } = new();

        // FK
        public int InvoiceId { get; }
        public int VariantId { get; }
    }
}
