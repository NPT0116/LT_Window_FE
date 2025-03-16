using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Responses;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository.ApiService
{
    public interface ICustomerApiService
    {
        Task<GetAllCustomerResponse> GetAllCustomers();
        Task CreateCustomer(CreateCustomerDto dto);
    }
}
