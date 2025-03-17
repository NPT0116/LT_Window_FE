using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query
{
    public class InvoiceQueryParameter
    {
        public InvoiceDatetimeQueryParameter? invoiceDatetimeQueryParameter { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
    }

    public class InvoiceDatetimeQueryParameter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string sortDirection { get; set; } = "desc";
    }

}