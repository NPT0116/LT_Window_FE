using PhoneSelling.Data.Common.Contracts.Requests;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.VariantRepository
{
    public class VariantRepository : IVariantRepository
    {
        private readonly IVariantApiService _apiSerivce;
        public VariantRepository()
        {
            _apiSerivce = DIContainer.GetKeyedSingleton<IVariantApiService>();
        }
        public async Task<PaginationResult<Variant>> GetAllVariants(VariantPaginationQuery query)
        {
            var response = await _apiSerivce.GetAllVariants(query);
            if(response != null && response.succeeded)
            {
                return new PaginationResult<Variant>
                {
                    Data = response.data.Select(dto => new Variant
                    {
                        VariantId = dto.variantId,
                        Storage = dto.storage,
                        CostPrice = dto.costPrice,
                        SellingPrice = dto.sellingPrice,
                        StockQuantity = dto.stockQuantity,
                        Item = new Item()
                        {
                            ItemName = dto.itemDto.itemName,
                            Description = dto.itemDto.description,
                            Picture = dto.itemDto.picture,
                            ReleaseDate = DateTime.Parse(dto.itemDto.releaseDate),
                            ItemGroupId = Guid.Parse(dto.itemDto.itemGroupID),
                            ManufacturerId = Guid.Parse(dto.itemDto.manufacturerID)
                        },
                        Color = new Color
                        {
                            ColorId = Guid.Parse(dto.colorDto.colorID),
                            Name = dto.colorDto.name,
                            UrlImage = dto.colorDto.urlImage,
                            ItemId = Guid.Parse(dto.colorDto.itemID)
                        }
                    }).ToList(),
                };
            }

            return new PaginationResult<Variant>
            {
                Data = new List<Variant> { }
            };
        }
    }
}
