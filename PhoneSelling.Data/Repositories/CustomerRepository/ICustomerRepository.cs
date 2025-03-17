using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<Customer> CreateQuickCustomerAsync(Customer customerDto);
        Task<Customer> GetCustomerByPhoneAsync(string phone);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<Customer> UpdateCustomerAsync( Customer customerDto);
        Task<ICollection<Customer>> GetAllCustomersAsync();
    }
}
