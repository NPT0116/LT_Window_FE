using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService.Contracts.Responses
{
    public class GetTransactionHistoryByVariantIDResponse
    {
        public List<TransactionDto> data { get; set; }
        public bool succeeded { get; set; }
        public List<string>? errors { get; set; }
        public string? message { get; set; }
    }
}
