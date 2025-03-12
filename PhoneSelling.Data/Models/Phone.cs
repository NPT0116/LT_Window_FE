using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Phone : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public long Price { get; private set; }

        public Phone(
            string name,
            long price)
        {
            Name = name;
            Price = price;
        }
    }
}
