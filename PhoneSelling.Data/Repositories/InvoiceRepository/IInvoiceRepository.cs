using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository
{
    public interface IInvoiceRepository
    {
        //Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto invoiceDto);
        //Task<InvoiceDto> GetInvoiceByIdAsync(Guid invoiceId);
        Task<IEnumerable<InvoiceDto>> GetAllInvoices();
        //Task<IEnumerable<InvoiceDto>> GetInvoicesByCustomer(Customer customer);
    }
}
