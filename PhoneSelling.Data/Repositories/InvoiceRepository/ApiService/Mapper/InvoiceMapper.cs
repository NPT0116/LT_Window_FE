using PhoneSelling.Common;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Mapper
{
    public static class InvoiceMapper
    {
        /// <summary>
        /// Mapping từ InvoiceDto sang domain model Invoice.
        /// </summary>
        public static Invoice MapToModel(InvoiceDto dto)
        {
            if (dto == null)
                return null;

            var invoice = new Invoice
            {
                Id = Guid.Parse(dto.invoiceID),
                TotalAmount = dto.totalAmount,
                Date = DateTime.Parse(dto.Date),
                CustomerID = Guid.Parse(dto.customerID),
                InvoiceDetails = new TrulyObservableCollection<InvoiceDetail>(
                    dto.InvoiceDetails.Select(detailDto => new InvoiceDetail
                    {
                        Id = Guid.Parse(detailDto.invoiceDetailID),
                        InvoiceId = Guid.Parse(dto.invoiceID), // Sử dụng invoiceID của hóa đơn cha
                        VariantId = Guid.Parse(detailDto.variantID),
                        Quantity = detailDto.quantity,
                        Price = detailDto.price
                    })
                )
            };

            return invoice;
        }

        /// <summary>
        /// Mapping từ domain model Invoice sang InvoiceDto.
        /// </summary>
        public static InvoiceDto MapToDto(Invoice invoice)
        {
            if (invoice == null)
                return null;

            var dto = new InvoiceDto
            {
                invoiceID = invoice.Id.ToString(),
                customerID = invoice.CustomerID.ToString(),
                totalAmount = invoice.TotalAmount,
                Date = invoice.Date.ToString("o"),  // Định dạng ISO 8601
                InvoiceDetails = invoice.InvoiceDetails.Select(detail => new InvoiceDetailDto
                {
                    invoiceDetailID = detail.Id.ToString(),
                    invoiceID = invoice.Id.ToString(),
                    variantID = detail.VariantId.ToString(),
                    quantity = detail.Quantity,
                    price = detail.Price
                }).ToList()
            };

            return dto;
        }

        /// <summary>
        /// Mapping từ danh sách InvoiceDto sang danh sách domain model Invoice.
        /// </summary>
        public static List<Invoice> MapToModelList(IEnumerable<InvoiceDto> dtos)
        {
            if (dtos == null)
                return new List<Invoice>();

            return dtos.Select(dto => MapToModel(dto)).ToList();
        }

        /// <summary>
        /// Mapping từ danh sách domain model Invoice sang danh sách InvoiceDto.
        /// </summary>
        public static List<InvoiceDto> MapToDtoList(IEnumerable<Invoice> invoices)
        {
            if (invoices == null)
                return new List<InvoiceDto>();

            return invoices.Select(invoice => MapToDto(invoice)).ToList();
        }
    }
}
