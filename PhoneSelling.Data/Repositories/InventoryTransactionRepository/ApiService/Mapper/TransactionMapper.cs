using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Mapper
{
    public static class TransactionMapper
    {
        public static CreateInboundTransactionRequest ToRequest(this CreateInboundTransactionDto dto)
        {
            return new CreateInboundTransactionRequest
            {
                variantId = dto.VariantId.ToString(),
                quantity = dto.Quantity
            };
        }
    }
}
