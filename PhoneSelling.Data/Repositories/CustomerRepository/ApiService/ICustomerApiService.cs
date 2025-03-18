using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Responses;
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
        Task CreateCustomer(CreateCustomerRequest dto);
        Task<ApiResponse<CustomerDto>> GetAllCustomersByPhone(string phoneNumber);
        Task<GetAllCustomerResponse> GetAllCustomersByEmail(string email);
    }
}
