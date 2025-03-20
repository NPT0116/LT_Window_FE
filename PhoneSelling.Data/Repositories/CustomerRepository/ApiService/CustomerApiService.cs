using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Responses;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository.ApiService
{
    public class CustomerApiService : BaseApiService, ICustomerApiService
    {
        protected override string Prefix => "Customer";

        public CustomerApiService() { }

        public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            try
            {
                // Giả sử endpoint tạo khách hàng là: {ApiUrl}/Create
                var response = await _httpClient.PostAsJsonAsync(ApiUrl + "/Create", request);
                var result = await response.Content.ReadFromJsonAsync<CreateCustomerResponse>();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating customer", ex);
            }
        }

        public async Task<GetAllCustomerResponse> GetAllCustomersAsync()
        {
            try
            {
                // Giả sử endpoint lấy tất cả khách hàng: {ApiUrl}/All
                var response = await _httpClient.GetFromJsonAsync<GetAllCustomerResponse>(ApiUrl + "/All");
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching all customers", ex);
            }
        }

        public async Task<GetCustomerByIdResponse> GetCustomerByIdAsync(Guid customerId)
        {
            try
            {
                string url = $"{ApiUrl}/{customerId}";
                var response = await _httpClient.GetFromJsonAsync<GetCustomerByIdResponse>(url);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching customer by id", ex);
            }
        }

        public async Task<GetCustomerByPhoneResponse> GetCustomerByPhoneAsync(string phone)
        {
            try
            {
                string url = $"{ApiUrl}/Phone/{phone}";
                var response = await _httpClient.GetFromJsonAsync<GetCustomerByPhoneResponse>(url);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching customer by phone", ex);
            }
        }

        public async Task<GetCustomerByEmailResponse> GetCustomerByEmailAsync(string email)
        {
            try
            {
                string url = $"{ApiUrl}/Email/{email}";
                var response = await _httpClient.GetFromJsonAsync<GetCustomerByEmailResponse>(url);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching customer by email", ex);
            }
        }

        public async Task<UpdateCustomerResponse> UpdateCustomerAsync(Guid customerId, UpdateCustomerRequest request)
        {
            try
            {
                string url = $"{ApiUrl}/{customerId}";
                var httpResponse = await _httpClient.PutAsJsonAsync(url, request);
                var response = await httpResponse.Content.ReadFromJsonAsync<UpdateCustomerResponse>();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating customer", ex);
            }
        }
    }
}
