using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.VariantRepository.ApiService.Common
{
    public class VariantDto
    {
        public string variantId {  get; set; }
        public string itemId { get; set; }
        public string colorId { get; set; }
        public string storage {  get; set; }
        public int costPrice { get; set; }
        public int sellingPrice { get; set; }
        public int stockQuantity { get; set; }
    }
}
