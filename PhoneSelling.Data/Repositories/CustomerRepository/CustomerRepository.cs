using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Requests;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
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
            var createCustomerDto = new CreateCustomerDto()
            {
                name = customer.Name,
                phone = customer.Phone,
                email = customer.Email,
                address = customer.Address
            };
            await _apiService.CreateCustomer(createCustomerDto);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var response = await _apiService.GetAllCustomers();
            return response.Data.Select(dto => new Customer
            {
                Name = dto.name,
                Email = dto.email,
                Phone = dto.phone,
                Address = dto.address,
            }).ToList();
        }
    }
}
