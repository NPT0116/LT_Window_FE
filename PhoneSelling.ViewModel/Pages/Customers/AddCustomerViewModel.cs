using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Customers
{
    public partial class AddCustomerViewModel : ValidatableViewModel
    {
        [ObservableProperty] private Customer customer = new();
        private readonly ICustomerRepository _customerRepository;
        public event Action? CustomerCreated; // ✅ Event to Notify Parent
        public event Action? RequestClose;    // ✅ Event to Close Modal


        public AddCustomerViewModel()
        {
            _customerRepository = DIContainer.GetKeyedSingleton<ICustomerRepository>();
        }

        private void OnNameChange()
        {
            
            ClearErrors(nameof(Customer.Name)); // ✅ Clear previous validation errors

            var errors = Customer.ValidateName(); // ✅ Get validation errors
            if (errors.Any())
            {
                AddError(nameof(Customer.Name), string.Join("\n", errors)); // ✅ Corrected `string.Join()`
            }
        }

        private void OnAddressChanged()
        {
            ClearErrors(nameof(Customer.Address));

            var errors = Customer.ValidateAddress();
            if(errors.Any())
            {
                AddError(nameof(Customer.Address), string.Join("\n", errors));
            }
        }

        private void OnPhoneChanged()
        {
            ClearErrors(nameof(Customer.Phone));

            var errors = Customer.ValidatePhone();
            if (errors.Any())
            {
                AddError(nameof(Customer.Phone), string.Join("\n", errors));
            }
        }

        [RelayCommand]
        public async Task CreateNewCustomer()
        {
            try
            {
                await _customerRepository.CreateCustomer(Customer);
                if (CustomerCreated != null)
                {
                    CustomerCreated.Invoke(); // Notify Parent ViewModel to Refresh List
                }

                // ✅ Close Modal (Assuming it's a ContentDialog)
                RequestClose?.Invoke();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
