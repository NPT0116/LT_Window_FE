using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.CustomerRepository.ApiService.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto MapToCustomerDto(Customer customer)
        {
            return new CustomerDto
            {
                customerID = customer.Id,
                name = customer.Name,
                email = customer.Email,
                phone = customer.Phone,
                address = customer.Address
            };
        }
        public static Customer MapToCustomer(CustomerDto customerDto)
        {
            return new Customer
            {
                Id = customerDto.customerID,
                Name = customerDto.name,
                Email = customerDto.email,
                Phone = customerDto.phone,
                Address = customerDto.address
            };
        }
        public static List<CustomerDto> MapToCustomerDtos(List<Customer> customers)
        {
            return customers.Select(c => MapToCustomerDto(c)).ToList();
        }
        public static List<Customer> MapToCustomers(List<CustomerDto> customerDtos)
        {
            return customerDtos.Select(c => MapToCustomer(c)).ToList();
        }   
    }
}
