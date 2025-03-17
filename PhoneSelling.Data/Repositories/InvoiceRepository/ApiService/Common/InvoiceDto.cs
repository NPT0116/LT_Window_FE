using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common
{
    public class InvoiceDto
    {
        public string invoiceID { get; set; }

        public string customerID { get; set; }

        public float totalAmount { get; set; }

        public string Date { get; set; }
        public List<InvoiceDetailDto> InvoiceDetails { get; set; }

    }
}
