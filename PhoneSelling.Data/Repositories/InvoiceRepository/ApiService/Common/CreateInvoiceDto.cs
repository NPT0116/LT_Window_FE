using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common
{
   public class CreateInvoiceDto
    {
        public string customerID { get; set; }
        public string date { get; set; }
        public float totalAmount { get; set; }

        [Required]
        public List<CreateInvoiceDetailDto> invoiceDetailDtos { get; set; }
    }
}
