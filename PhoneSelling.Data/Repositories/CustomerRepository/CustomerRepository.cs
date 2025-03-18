using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Responses;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerApiService _apiService;
        public CustomerRepository()
        {
            _apiService = DIContainer.GetKeyedSingleton<ICustomerApiService>();
        }

        public async Task CreateCustomer(Customer customer)
        {
            var createCustomerRequest = new ApiService.Contracts.Requests.CreateCustomerRequest()
            {
                name = customer.Name,
                phone = customer.Phone,
                email = customer.Email,
                address = customer.Address
            };
            await _apiService.CreateCustomer(createCustomerRequest);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var response = await _apiService.GetAllCustomers();
            return ConvertListCustomerReposonseToListCustomer(response);
        }

        public async Task<List<Customer>> GetAllCustomersByEmail(string email)
        {
            var response = await _apiService.GetAllCustomersByEmail(email);
            return ConvertListCustomerReposonseToListCustomer(response);
        }

        public async Task<Customer?> GetCustomerByPhone(string phone)
        {
            var response = await _apiService.GetAllCustomersByPhone(phone);
            if (response == null) return null;
            var customerDto = response.Data;
            if (customerDto == null) return null;
            return new Customer
            {
                CustomerID = Guid.Parse(customerDto.customerID),
                Name = customerDto.name,
                Email = customerDto.email,
                Phone = customerDto.phone,
                Address = customerDto.address
            };
        }

        private List<Customer> ConvertListCustomerReposonseToListCustomer(GetAllCustomerResponse customerReposonse)
        {
            if (customerReposonse == null || customerReposonse.Data == null || customerReposonse.Data.Count == 0) return new List<Customer>();
            return customerReposonse.Data.Select(dto => new Customer
            {
                CustomerID = Guid.Parse(dto.customerID),
                Name = dto.name,
                Email = dto.email,
                Phone = dto.phone,
                Address = dto.address,
            }).ToList();
        }
    }
}
