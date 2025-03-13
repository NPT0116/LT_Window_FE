using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts
{
    public class ItemListDto
    {
        public Guid itemId { get; set; }
        public Guid itemGroupId { get; set; }
        public string itemName { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public DateTime ReleasedDate { get; set; }
        public Guid manufacturerId { get; set; }
    }
}
