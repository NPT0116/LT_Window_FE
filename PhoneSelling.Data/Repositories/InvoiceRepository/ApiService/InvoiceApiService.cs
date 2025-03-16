using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService
{
    public class InvoiceApiService : BaseApiService, IInvoiceApiService
    {
        protected override string Prefix => "Invoice";

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<GetAllInvoiceReponse>(ApiUrl);
            return response.Data.Select(dto => new Invoice
            {
                TotalAmount = dto.totalAmount,
                Date = DateTime.Parse(dto.Date),
                CustomerId = Guid.Parse(dto.customerID),
                InvoiceDetails = new ObservableCollection<InvoiceDetail>(
                dto.InvoiceDetails.Select(invoiceDetailDto => new InvoiceDetail
                {
                    Quantity = invoiceDetailDto.quantity,
                    Price = invoiceDetailDto.price,
                    InvoiceId = Guid.Parse(dto.invoiceID),
                    VariantId = Guid.Parse(invoiceDetailDto.variantID)
                })
            )

            }).ToList();
        }
    }
}
