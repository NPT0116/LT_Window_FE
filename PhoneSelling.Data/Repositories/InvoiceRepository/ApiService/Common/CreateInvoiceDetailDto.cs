using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common
{
   public class CreateInvoiceDetailDto
    {
        public string variantID { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
    }
}
