using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class ItemGroup : BaseEntity
    {
        public string ItemGroupName { get; } = string.Empty;
        public string Category { get; } = string.Empty;
        public List<Item> Items { get; set; } = new();
    }
}
