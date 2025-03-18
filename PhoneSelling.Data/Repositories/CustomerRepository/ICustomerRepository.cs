using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task CreateCustomer(Customer customer);
        Task<Customer?> GetCustomerByPhone(string phone);
        Task<List<Customer>> GetAllCustomersByEmail(string email);
    }
}
