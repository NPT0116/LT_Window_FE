using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Mapper;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
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

        public async Task<Customer> CreateQuickCustomerAsync(Customer customer)
        {
            // Map từ domain model Customer sang CreateCustomerRequest
            var request = new CreateCustomerRequest
            {
                name = customer.Name,
                phone = customer.Phone,
                email = customer.Email,
                address = customer.Address
            };

            // Gọi API tạo khách hàng
            var response = await _apiService.CreateCustomerAsync(request);
            if (response == null || !response.Succeeded)
                throw new Exception(response?.Message ?? "Error while creating customer.");

            // Mapping từ response (assumed to have Data property of type CustomerResponseDto) sang domain model Customer
            var dto = response.Data;
            var createdCustomer = CustomerMapper.MapToCustomer(dto);
            return createdCustomer;
        }

        public async Task<ICollection<Customer>> GetAllCustomersAsync()
        {
            var response = await _apiService.GetAllCustomersAsync();
            if (response == null || response.Data == null)
                return new List<Customer>();

            return CustomerMapper.MapToCustomers(response.Data);
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            var response = await _apiService.GetCustomerByIdAsync(customerId);
            if (response == null)
                throw new Exception($"Internal Sever Error");
            if (!response.Succeeded)
                throw new Exception(response.Message);

            var dto = response.Data;
            var customer = CustomerMapper.MapToCustomer(dto);
            return customer;
        }

        public async Task<Customer> GetCustomerByPhoneAsync(string phone)
        {
            var response = await _apiService.GetCustomerByPhoneAsync(phone);
            if (response == null)
                throw new Exception($"Internal Sever Error");
            if (!response.Succeeded)
                throw new Exception(response.Message);

            var dto = response.Data;
            var customer = CustomerMapper.MapToCustomer(dto);
            return customer;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var response = await _apiService.GetCustomerByEmailAsync(email);
            Debug.WriteLine(JsonSerializer.Serialize(response));
            if (response == null )
                throw new Exception($"Internal Sever Error");
            if (!response.Succeeded)
                throw new Exception(response.Message);
            var dto = response.Data;
            var customer = CustomerMapper.MapToCustomer(dto);
            Debug.WriteLine(JsonSerializer.Serialize(customer));
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            // Map từ domain model Customer sang UpdateCustomerRequest
            var request = new UpdateCustomerRequest
            {
                name = customer.Name,
                phone = customer.Phone,
                email = customer.Email,
                address = customer.Address
            };

            var response = await _apiService.UpdateCustomerAsync(customer.Id, request);
            if (response == null)
                throw new Exception($"Internal sever error");
            if (!response.Succeeded)
                throw new Exception(response.Message);

            var dto = response.Data;
            var updatedCustomer = CustomerMapper.MapToCustomer(dto);
            return updatedCustomer;
        }
    }
}
