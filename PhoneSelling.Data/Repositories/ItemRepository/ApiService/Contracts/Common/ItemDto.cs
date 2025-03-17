using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common
{
    public class ItemDto
    {
        public string itemID { get; set; }
        public string itemGroupID { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public string? picture { get; set; }
        public string releaseDate { get; set; }
        public string manufacturerID { get; set; }
    }
}
