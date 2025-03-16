using PhoneSelling.Data.Repositories.Abstraction;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Requests;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService.Contracts.Responses;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository.ApiService
{
    public class CustomerApiService : BaseApiService, ICustomerApiService
    {
        public CustomerApiService()
        {
        }

        protected override string Prefix => "Customer";

        public async Task CreateCustomer(CreateCustomerDto dto)
        {
            try
            {
                await _httpClient.PostAsJsonAsync<CreateCustomerDto>(ApiUrl + "/Create", dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating customer: {ex.Message}");
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
    }
}
