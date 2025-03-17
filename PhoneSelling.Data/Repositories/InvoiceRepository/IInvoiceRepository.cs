using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository
{
    public interface IInvoiceRepository
    {
        Task<bool> CreateInvoiceAsync(Invoice CreateInvoiceRequest);
        Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId);

        Task<PaginationResult<Invoice>> GetAllInvoices(InvoiceQueryParameter invoiceQueryParameter);
        Task<IEnumerable<Invoice>> GetInvoicesByCustomer(Guid CustomerId);
        Task<byte[]> GetInvoicePdfPrintAsync(Guid invoiceId);

    }
}
