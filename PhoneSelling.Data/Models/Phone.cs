using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Phone : BaseEntity
    {
        public string Name { get; } = string.Empty;
        public long Price { get; }

        public Phone(
            string name,
            long price)
        {
            Name = name;
            Price = price;
        }
    }
}
