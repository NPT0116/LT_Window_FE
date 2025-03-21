using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Requests
{
    public class CreateMultipleInboundTransactionRequest
    {
        public List<CreateInboundTransactionRequest> list { get; set; }
    }
}
