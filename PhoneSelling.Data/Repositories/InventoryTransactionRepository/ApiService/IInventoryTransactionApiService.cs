using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService
{
    public interface IInventoryTransactionApiService
    {
        Task CreateInboundInventoryTransaction(CreateInboundTransactionRequest request);
    }
}
