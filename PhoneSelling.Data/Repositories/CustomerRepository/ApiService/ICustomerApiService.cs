using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Common;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Requests;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository.ApiService
{
    public interface ICustomerApiService
    {
        // Tạo khách hàng
        Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request);

        // Lấy tất cả khách hàng
        Task<GetAllCustomerResponse> GetAllCustomersAsync();

        // Lấy khách hàng theo id
        Task<GetCustomerByIdResponse> GetCustomerByIdAsync(Guid customerId);

        // Lấy khách hàng theo số điện thoại
        Task<GetCustomerByPhoneResponse> GetCustomerByPhoneAsync(string phone);

        // Lấy khách hàng theo email
        Task<GetCustomerByEmailResponse> GetCustomerByEmailAsync(string email);

        // Cập nhật khách hàng
        Task<UpdateCustomerResponse> UpdateCustomerAsync(Guid customerId, UpdateCustomerRequest request);
    }
}
