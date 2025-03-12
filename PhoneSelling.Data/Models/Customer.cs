using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Customer : BaseEntity
    {
        public string Name { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string Phone { get; } = string.Empty;
        public string Address { get; } = string.Empty;
        public List<Invoice> Invoices { get; set; } = new();
    }
}
