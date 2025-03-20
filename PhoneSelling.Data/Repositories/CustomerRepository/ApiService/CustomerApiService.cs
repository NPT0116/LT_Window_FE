using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Responses;
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
        public CustomerApiService()
        {
        }

        protected override string Prefix => "Customer";

        public async Task<bool> CreateCustomer(CreateCustomerRequest dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CreateCustomerRequest>(ApiUrl + "/Create", dto);
                // 🔹 Read the response body
                var responseBody = await response.Content.ReadAsStringAsync();

                // 🔹 Deserialize into `ApiResponse<object>` since "data" is null in this case
                var result = JsonSerializer.Deserialize<ApiResponse<object>>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // ✅ Ensures JSON properties are matched case-insensitively
                });

                // 🔹 Debugging output
                Debug.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));

                // 🔹 Check for errors
                if (result != null && !result.Succeeded)
                {
                    Debug.WriteLine($"Error: {string.Join(", ", result.Errors)}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating customer: {ex.Message}");
                return false;
            }

        }

        public async Task<GetAllCustomerResponse> GetAllCustomers()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<GetAllCustomerResponse>(ApiUrl + "/All");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching customers: {ex.Message}");
                return null; // Return empty list on failure
            }
        }

        public async Task<ApiResponse<CustomerDto>> GetAllCustomersByEmail(string email)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<CustomerDto>>(ApiUrl + $"/Email/{email}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching customers: {ex.Message}");
                return null; // Return empty list on failure
            }
        }

        public async Task<ApiResponse<CustomerDto>> GetAllCustomersByPhone(string phoneNumber)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<CustomerDto>>(ApiUrl + $"/Phone/{phoneNumber}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching customers: {ex.Message}");
                return null; // Return empty list on failure
            }
        }
    }
}
