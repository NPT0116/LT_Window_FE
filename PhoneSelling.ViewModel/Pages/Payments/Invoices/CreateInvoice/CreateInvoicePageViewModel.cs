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

namespace PhoneSelling.ViewModel.Pages.Payments.Invoices
{
    public partial class CreateInvoicePageViewModel : BasePageViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IVariantRepository _variantRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        [ObservableProperty] private Customer customer;
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
            };
           
        }

        public async Task<Customer?> SearchCustomersByEmail(string email)
        {
            var customer = await _customerRepository.GetCustomerByEmail(email);
            return customer;
        }

        public async Task<Customer?> SearchCustomerByPhone(string phoneNumber)
        {
            Debug.WriteLine("SearchCustomersByPhone called with: " + phoneNumber);

            var response = await _customerRepository.GetCustomerByPhone(phoneNumber);
            Debug.WriteLine("Response received from API call.");

            return response;
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
                InvoiceId = Invoice.InvoiceID,
                Quantity = 1,
                Price = 100, // Example default value
            };

            Invoice.InvoiceDetails.Add(newDetail); // Ensure we update the existing collection
            Debug.WriteLine($"Added: Quantity={newDetail.Quantity}, Price={newDetail.Price}, number of details: {Invoice.InvoiceDetails.Count}");
        }

        public void RecalculateTotal()
        {
            float total = 0;
            Debug.WriteLine(DateTime.Now.ToString());
            Debug.WriteLine($"InvoiceDetails Count Before Calculation: {Invoice.InvoiceDetails.Count}");

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
            await _invoiceRepository.CreateInvoiceAsync(Invoice);
        }
    }
}
