using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Invoice : BaseEntity
    {
        public float TotalAmount { get; set; }
        public DateTime Date { get; set; }

        // FK
        public Guid CustomerId { get; set; }
    }
}
