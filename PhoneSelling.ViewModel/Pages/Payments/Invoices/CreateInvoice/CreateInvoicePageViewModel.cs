using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Common;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.Data.Repositories.InvoiceRepository;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PhoneSelling.ViewModel.Pages.Payments.Invoices
{
    public partial class CreateInvoicePageViewModel : BasePageViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IVariantRepository _variantRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        [ObservableProperty] private Customer customer = new();
        [ObservableProperty] private Invoice invoice;
        [ObservableProperty] private TrulyObservableCollection<Variant> searchItems;
        //[ObservableProperty] private Variant variant = new();
        //public string VariantDisplayName => Variant != null ? $"{Variant.Item.ItemName} {Variant.Storage} {Variant.Color.Name}" : "";

        //partial void OnVariantChanged(Variant oldValue, Variant newValue)
        //{
        //    OnPropertyChanged(nameof(VariantDisplayName)); // Notify UI to update name
        //}
        public CreateInvoicePageViewModel()
        {
            _customerRepository = DIContainer.GetKeyedSingleton<ICustomerRepository>();
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();
            _invoiceRepository = DIContainer.GetKeyedSingleton<IInvoiceRepository>();
            Invoice = new()
            {
                InvoiceDetails = new TrulyObservableCollection<InvoiceDetail>()
                {
                    new InvoiceDetail() {}
                }
            };
            Debug.WriteLine(Invoice.Date);
           
        }

        public async Task<Customer?> SearchCustomersByEmail(string email)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            return customer;
        }

        public async Task<Customer?> SearchCustomerByPhone(string phoneNumber)
        {
            Debug.WriteLine("SearchCustomersByPhone called with: " + phoneNumber);

            var response = await _customerRepository.GetCustomerByPhoneAsync(phoneNumber);
            Debug.WriteLine("Response received from API call.");

            return response;
        }

        


        public async Task CreateCustomer()
        {
            await _customerRepository.CreateQuickCustomerAsync(Customer);
            Customer = new();
        }

        public async Task<List<Variant>> SearchVariants(string text)
        {
            var query = new VariantPaginationQuery
            {
                PageNumber = 1,
                PageSize = 5,
                SearchKey = text
            };
            var paginationResult = await _variantRepository.GetAllVariants(query);
            return paginationResult.Data;
        }

        [RelayCommand]
        public void AddInvoiceDetail()
        {
            var newDetail = new InvoiceDetail
            {
                InvoiceID = Invoice.InvoiceID,
                Quantity = 1,
                Price = 100, // Example default value
            };

            Invoice.InvoiceDetails.Add(newDetail); // Ensure we update the existing collection
        }

        public void RecalculateTotal()
        {
            float total = 0;           
            foreach (var detail in Invoice.InvoiceDetails)
            {
                Debug.WriteLine($"Quantity: {detail.Quantity}, Price: {detail.Price}");
                total += detail.Quantity * detail.Price;
            }

            Debug.WriteLine($"Final Total Amount: {total}");
            Invoice.TotalAmount = total;
        }

        [RelayCommand]
        public async Task CreateInvoice()
        {
            var hasErrors = Invoice.Validate();
            foreach(var detail in Invoice.InvoiceDetails)
            {
                var validationResult = detail.Validate();
                if(validationResult) hasErrors = true;
            }
            if(!hasErrors) await _invoiceRepository.CreateInvoiceAsync(Invoice);
        }
    }
}
