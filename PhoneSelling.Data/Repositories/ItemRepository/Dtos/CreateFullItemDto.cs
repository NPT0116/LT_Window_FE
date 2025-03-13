using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemRepository.Dtos
{
    public class CreateFullItemDto
    {
        public Item Item { get; set; }
        public List<Color> Colors { get; set; }
        public List<Variant> Variants { get; set; }
    }
}
