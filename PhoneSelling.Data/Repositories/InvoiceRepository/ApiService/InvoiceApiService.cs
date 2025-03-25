using Microsoft.AspNetCore.WebUtilities;
using PhoneSelling.Common;
using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService
{
    public class InvoiceApiService : BaseApiService, IInvoiceApiService
    {
        protected override string Prefix => "invoices";

        public async Task<CreateInvoiceResponse> CreateInvoiceAsync(CreateInvoiceDto createInvoiceRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ApiUrl, createInvoiceRequest);
                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Raw JSON Response: {responseBody}");

                var CreateInvoiceResponse = await response.Content.ReadFromJsonAsync<CreateInvoiceResponse>();
                return CreateInvoiceResponse;
            }
            catch (Exception ex)
            {
                // Quăng exception chứa thông tin lỗi cho tầng view model xử lý
                throw new Exception("Error while creating invoice", ex);
            }
        }

        public async Task<GetAllInvoiceReponse> GetAllInvoicesAsync(InvoiceQueryParameter queryParameters)
        {
            try
            {
                // Xây dựng dictionary chứa các query parameters
                var queryParams = new Dictionary<string, string>();

                if (queryParameters.InvoiceDatetimeQueryParameter != null)
                {
                    if (queryParameters.InvoiceDatetimeQueryParameter.FromDate.HasValue)
                    {
                        // Sử dụng định dạng ISO 8601 cho ngày
                        queryParams.Add("invoiceDatetimeQueryParameter.fromDate", queryParameters.InvoiceDatetimeQueryParameter.FromDate.Value.ToString("o"));
                    }
                    if (queryParameters.InvoiceDatetimeQueryParameter.ToDate.HasValue)
                    {
                        queryParams.Add("invoiceDatetimeQueryParameter.toDate", queryParameters.InvoiceDatetimeQueryParameter.ToDate.Value.ToString("o"));
                    }
                    queryParams.Add("sortDirection", EnumHelper.GetEnumMemberValue(queryParameters.SortDirection));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.CustomerName))
                {
                    queryParams.Add("customerName", queryParameters.CustomerName);
                }
                if (!string.IsNullOrWhiteSpace(queryParameters.CustomerPhone))
                {
                    queryParams.Add("customerPhone", queryParameters.CustomerPhone);
                }

                // Nối query string vào ApiUrl
                var urlWithQuery = QueryHelpers.AddQueryString(ApiUrl, queryParams);
                Debug.WriteLine(urlWithQuery);
                var response = await _httpClient.GetFromJsonAsync<GetAllInvoiceReponse>(urlWithQuery);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching invoices", ex);
            }
        }

        public async Task<GetInvoiceByIdResponse> GetInvoiceByIdAsync(Guid invoiceId)
        {
            try
            {
                // Giả sử endpoint cho GetInvoiceById là: {ApiUrl}/{invoiceId}
                string url = $"{ApiUrl}/{invoiceId.ToString()}";
                var response = await _httpClient.GetFromJsonAsync<GetInvoiceByIdResponse>(url);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching invoice by id", ex);
            }
        }


        public async Task<GetInvoicesByCustomerIdResponse> GetInvoicesByCustomerIdAsync(Guid customerId)
        {
            string url = $"{ApiUrl}/customer/{customerId}";
            try
            {
                var response = await _httpClient.GetFromJsonAsync< GetInvoicesByCustomerIdResponse>(url);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching invoices by customer id", ex);
            }
        }
        public async Task<byte[]> GetInvoicePdfPrintAsync(Guid invoiceId)
        {
            try
            {
                Debug.WriteLine($"Export invoice {invoiceId}");
                // Giả sử endpoint là: {ApiUrl}/{invoiceId}/pdf-print
                string url = $"{ApiUrl}/{invoiceId.ToString()}/pdf-print";
                return await _httpClient.GetByteArrayAsync(url);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching invoice PDF print", ex);
            }
        }
        public async Task<byte[]> GetInvoicePdfPrintElectronicAsync(Guid invoiceId)
        {
            try
            {
                Debug.WriteLine($"Export invoice (electronic) {invoiceId}");

                string url = $"{ApiUrl}/{invoiceId.ToString()}/pdf-electronic";
                return await _httpClient.GetByteArrayAsync(url);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching invoice PDF-elctronic print", ex);
            }
        }
    }
}
