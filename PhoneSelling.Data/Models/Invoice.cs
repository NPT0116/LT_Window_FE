using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Common;
using PhoneSelling.Data.Common.Validators;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PhoneSelling.Data.Models
{
    public partial class Invoice : BaseEntity
    {
        [ObservableProperty] private Guid invoiceID;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasErrors))]
        [Range(0.01, float.MaxValue, ErrorMessage = "Tổng lượng tiền phải lớn hơn 0")]
        private float totalAmount;
        
        [ObservableProperty]
        [NotDefault(ErrorMessage = "Ngày tạo hóa đơn không được để trống")]
        [NotifyPropertyChangedFor(nameof(HasErrors))]
        private DateTime date;
        
        [ObservableProperty]
        [MinCollectionCount(1, ErrorMessage = "Danh sách hóa đơn chi tiết không được để trống")]
        [NotifyPropertyChangedFor(nameof(HasErrors))]
        private TrulyObservableCollection<InvoiceDetail> invoiceDetails = new();
        // Foreign Key
        
        [ObservableProperty]
        [NotDefault(ErrorMessage = "Khách hàng không được để trống")]
        private Guid customerID;

        // Error messages
        [ObservableProperty] private string totalAmountError = string.Empty;
        [ObservableProperty] private string dateError = string.Empty;
        [ObservableProperty] private string invoiceDetailsError = string.Empty;
        [ObservableProperty] private string customerIDError = string.Empty;

        partial void OnInvoiceDetailsChanged(TrulyObservableCollection<InvoiceDetail> newValue)
        {
            Debug.WriteLine("Invoice detail validation is called");
            ValidateProperty(newValue, nameof(InvoiceDetails));
            InvoiceDetailsError = GetFirstError(nameof(InvoiceDetails));

            foreach(var detail in InvoiceDetails)
            {
                detail.Validate();
            }
        }

        partial void OnTotalAmountChanged(float newValue)
        {
            ValidateProperty(newValue, nameof(TotalAmount));
            TotalAmountError = GetFirstError(nameof(InvoiceDetails));
        }

        partial void OnDateChanged(DateTime newValue)
        {
            ValidateProperty(newValue, nameof(Date));
            DateError = GetFirstError(nameof(Date));
        }

        partial void OnCustomerIDChanged(Guid newCustomerID)
        {
            ValidateProperty(newCustomerID, nameof(CustomerID));
            CustomerIDError = GetFirstError(nameof(CustomerID));
        }

        public bool Validate()
        {
            ValidateAllProperties();

            InvoiceDetailsError = GetFirstError(nameof(InvoiceDetails));
            TotalAmountError = GetFirstError(nameof(TotalAmount));
            DateError = GetFirstError(nameof(Date));
            CustomerIDError = GetFirstError(nameof(CustomerID));

            return HasErrors;
        }
    }
}
