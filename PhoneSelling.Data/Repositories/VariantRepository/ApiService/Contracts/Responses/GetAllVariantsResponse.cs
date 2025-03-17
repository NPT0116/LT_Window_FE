using PhoneSelling.Data.Common.Contracts.Responses;
using PhoneSelling.Data.Repositories.ColorRepository.ApiService.Common;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Responses
{
    public class VariantDto
    {
        public string variantId { get; set; }
        public ItemDto itemDto { get; set; }
        public ColorDto colorDto { get; set; }
        public string storage {  get; set; }
        public int costPrice { get; set; }
        public int sellingPrice { get; set; }
        public int stockQuantity { get; set; }
    }
    public class GetAllVariantsResponse : PaginationApiResponse<VariantDto>
    {

    }
}
