using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Mapper;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IInvoiceApiService _invoiceApiService;
        public InvoiceRepository()
        {
            _invoiceApiService = DIContainer.GetKeyedSingleton<IInvoiceApiService>();
        }

        public async Task<bool> CreateInvoiceAsync(Invoice createInvoiceRequest)
        {
            // Chuyển đổi đối tượng Invoice sang CreateInvoiceDto để gửi cho API
            var createInvoiceDto = new CreateInvoiceDto
            {
                customerID = createInvoiceRequest.CustomerID.ToString(),
                date = createInvoiceRequest.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                totalAmount = createInvoiceRequest.TotalAmount,
                invoiceDetailDtos = createInvoiceRequest.InvoiceDetails.Select(invoiceDetail => new CreateInvoiceDetailDto
                {
                    price = invoiceDetail.Price,
                    quantity = invoiceDetail.Quantity,
                    variantID = invoiceDetail.VariantId.ToString()
                }).ToList()
            };

                // Gọi API tạo hóa đơn
                var createInvoiceResponse = await _invoiceApiService.CreateInvoiceAsync(createInvoiceDto);

                // Nếu API báo lỗi (Succeeded == false) thì throw exception chứa thông báo lỗi từ API
                if (!createInvoiceResponse.Succeeded)
                {
                    var errorMessage = createInvoiceResponse.Message ?? "Error while creating invoice.";
                    throw new Exception(errorMessage);
                }    

                return true;   
        }


        public async Task<PaginationResult<Invoice>> GetAllInvoices(InvoiceQueryParameter invoiceQueryParameter)
        {
            var response = await _invoiceApiService.GetAllInvoicesAsync(invoiceQueryParameter);
            var result = new PaginationResult<Invoice>
            {
                Data = InvoiceMapper.MapToModelList(response.data),
                PageNumber = response.pageNumber,
                PageSize = response.pageSize,
                TotalPages = response.totalPages,
                TotalRecords = response.totalRecords

            };
            return result;
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId)
        {
            var response = await _invoiceApiService.GetInvoiceByIdAsync(invoiceId);
            if (response == null) return null;
            var invoiceDto = response.Data;
            var result = InvoiceMapper.MapToModel(invoiceDto);
            return result;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByCustomer(Guid CustomerId)
        {
             var response = await _invoiceApiService.GetInvoicesByCustomerIdAsync(CustomerId);
            if (response == null || response.Data.Count() == 0) return Enumerable.Empty<Invoice>();
            var invoiceDto = response.Data;
            var result = InvoiceMapper.MapToModelList(invoiceDto);
            return result;

        }
        public async Task<byte[]> GetInvoicePdfPrintAsync(Guid invoiceId)
        {
            return await _invoiceApiService.GetInvoicePdfPrintAsync(invoiceId);
        }
    }
}
