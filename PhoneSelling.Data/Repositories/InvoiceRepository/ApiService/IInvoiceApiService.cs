using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService
{
    public interface IInvoiceApiService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
    }
}
