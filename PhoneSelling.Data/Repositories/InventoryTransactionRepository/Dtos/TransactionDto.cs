using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos
{
    public class TransactionDto
    {
        public string  transactionID {get;set;}
        public string variantID { get; set; }
        public  int transactionType { get; set; }
        public int quantity { get; set; }
        public string transactionDate { get; set; }
        public string? invoiceDetailID { get; set; }
    }
}
