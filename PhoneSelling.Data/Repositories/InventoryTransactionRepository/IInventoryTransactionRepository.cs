using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository
{
    public interface IInventoryTransactionRepository
    {
        Task CreateInboundTransaction(CreateInboundTransactionDto dto);
        Task CreateInboundTransaction(List<CreateInboundTransactionDto> dto);
    }
}
