using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class Item : BaseEntity
    {
        public string ItemName { get; } = string.Empty;
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public DateTime ReleasedDate { get; set; }
        public List<Variant> Variants { get; set; } = new();

        //FK
        public required int ItemGroupId { get; set; }
        public required int ManufacturerId { get; set; }

    }
}
