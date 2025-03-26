using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Models;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository
{
    public interface IInventoryTransactionRepository
    {
        Task<List<HistoryTransaction>> GetTransactionHistoryByVariantID(Guid variantID);
        Task CreateInboundTransaction(CreateInboundTransactionDto dto);
        Task CreateInboundTransaction(List<CreateInboundTransactionDto> dto);
    }
}
