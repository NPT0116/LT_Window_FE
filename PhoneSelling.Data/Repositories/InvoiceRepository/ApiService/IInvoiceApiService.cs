using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService
{
    public interface IInvoiceApiService
    {
        Task<GetAllInvoiceReponse> GetAllInvoicesAsync(InvoiceQueryParameter invoiceQueryParameter);
        Task<CreateInvoiceResponse> CreateInvoiceAsync(CreateInvoiceDto CreateInvoiceRequest);
        Task<GetInvoiceByIdResponse> GetInvoiceByIdAsync(Guid invoiceId);
        Task<GetInvoicesByCustomerIdResponse> GetInvoicesByCustomerIdAsync(Guid customerId);

    }
}
