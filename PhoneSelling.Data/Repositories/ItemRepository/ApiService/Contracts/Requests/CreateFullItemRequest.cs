using PhoneSelling.Data.Repositories.ColorRepository.ApiService.Common;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests
{
    public class VariantDto
    {
        public string storage {  get; set; }
        public int costPrice { get; set; }
        public int sellingPrice { get; set; }
        public int stockQuantity { get; set; }
        public int colorTempId { get; set; }
    }
    public class ColorDto
    {
        public int tempId { get; set; }
        public string name { get; set; }
        public string urlImage { get; set; }
        public string colorId { get; set; }
    }
    public class CreateFullItemRequest
    {
        public ItemDto item { get; set; }
        public List<ColorDto> colors { get; set; }
        public List<VariantDto> variants { get; set; }
    }
}
