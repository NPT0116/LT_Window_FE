using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos
{
    public class CreateInboundTransactionDto
    {
        public Guid VariantId { get; set; }
        public int Quantity { get; set; }
    }
}
