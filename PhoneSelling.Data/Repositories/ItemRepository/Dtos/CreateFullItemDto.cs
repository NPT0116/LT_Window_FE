using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.Dtos
{
    public class ColorGroup
    {
        public Color Color { get; set; }
        public List<Variant> RelatedVariants { get; set; }
    }
    public class CreateFullItemDto
    {
        public ItemDto Item { get; set; }
        public List<ColorDto> Colors { get; set; }
        public List<VariantDto> Variants { get; set; }
    }
}
