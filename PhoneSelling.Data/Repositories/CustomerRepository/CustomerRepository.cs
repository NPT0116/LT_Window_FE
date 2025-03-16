using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<List<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }
    }
}
