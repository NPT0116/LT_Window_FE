using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Adapters
{
    public class ItemAdapter
    {
        public static List<Item> ConvertToItems(List<ItemListDto> dto)
        {
            return dto.Select(itemListDto => new Item
            {
                ItemGroupId = itemListDto.itemGroupId,
                ManufacturerId = itemListDto.manufacturerId,
                ItemName = itemListDto.itemName,
                Description = itemListDto.Description,
                Picture = itemListDto.Picture,
                ReleasedDate = itemListDto.ReleasedDate
            }).ToList();
        }
    }
}
