using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PhoneSelling.Common;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.Data.Repositories.InvoiceRepository;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Navigation;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace PhoneSelling.ViewModel.Pages.Payments.Invoices
{
    public record Message(string message, bool status);
    public partial class CreateInvoicePageViewModel : BasePageViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IVariantRepository _variantRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        [ObservableProperty] private Customer customer = new();
        [ObservableProperty] private Invoice invoice;
        [ObservableProperty] private TrulyObservableCollection<Variant> searchItems;


        public CreateInvoicePageViewModel()
        {
            _customerRepository = DIContainer.GetKeyedSingleton<ICustomerRepository>();
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();
            _invoiceRepository = DIContainer.GetKeyedSingleton<IInvoiceRepository>();
            Invoice = new();
            AddInvoiceDetail();
            Invoice.InvoiceDetails.CollectionChanged += InvoiceDetails_CollectionChanged;
        }
        private void InvoiceDetails_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach(var newItem in e.NewItems)
                {
                    Debug.WriteLine("Item added: " + newItem);
                }
            } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach(var oldItem in e.OldItems) 
                {
                    Debug.WriteLine("Item removed: " + oldItem);
                }
            }
            Invoice.ValidateInvoiceDetails();
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
            Customer.Email = Customer.Email.ToLower();
            Customer = await _customerRepository.CreateQuickCustomerAsync(Customer);
        }

        [RelayCommand]
        public void AddInvoiceDetail()
        {
            var newDetail = new InvoiceDetail
            {
                InvoiceID = Invoice.InvoiceID,
                Quantity = 1,
                Price = 0,
                Variant = new Variant()
            };
            RecalculateTotal();
            newDetail.RecalculateCallback = RecalculateTotal;
            newDetail.Variant.Id = Guid.Empty;
            newDetail.Variant.VariantID = Guid.Empty;
            Invoice.InvoiceDetails.Add(newDetail);
        }
        [RelayCommand]
        public void RemoveInvoiceDetail(InvoiceDetail detail)
        {
            Invoice.InvoiceDetails.Remove(detail); // Ensure we update the existing collection
            RecalculateTotal();
        }
        [RelayCommand]
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
            if (!hasErrors)
            {
                try
                {
                    await _invoiceRepository.CreateInvoiceAsync(Invoice);
                    Debug.WriteLine("Create Invoice Succesfully !");
                    WeakReferenceMessenger.Default.Send(new Message("TẠO HÓA ĐƠN THÀNH CÔNG !", true));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error while creating invoice:", ex);
                    WeakReferenceMessenger.Default.Send(new Message(ex.Message, false));
                }
            }
        }
    }
}
