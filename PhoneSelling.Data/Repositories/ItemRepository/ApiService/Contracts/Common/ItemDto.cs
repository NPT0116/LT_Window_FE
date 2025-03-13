using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common
{
    public class ItemDto
    {
        public string itemId { get; set; }
        public string itemGroupId { get; set; }
        public string itemName { get; set; }
        public string Description { get; set; }
        public string? picture { get; set; }
        public string releasedDate { get; set; }
        public string manufacturerId { get; set; }
    }
}
