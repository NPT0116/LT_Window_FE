using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Customers
{
    public partial class CustomerViewModel : BasePageViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        public PaginationQueryViewModel<Customer> QueryViewModel;
        public CustomerViewModel()
        {
            _customerRepository = DIContainer.GetKeyedSingleton<ICustomerRepository>();
            QueryViewModel = new(LoadDataAsync);
            QueryViewModel.LoadDataCommand.Execute(null);
        }

        public async Task<IEnumerable<Customer>> LoadDataAsync()
        {
            var customers = await _customerRepository.GetAllCustomers();
            return customers;
        }

        public async Task OnCustomerAdded()
        {
            var customers = await _customerRepository.GetAllCustomers();
            QueryViewModel.Items.Clear();
            foreach (var customer in customers)
            {
                QueryViewModel.Items.Add(customer);
            }
        }

    }
}
