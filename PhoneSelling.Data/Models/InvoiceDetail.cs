using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Common.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PhoneSelling.Data.Models
{
    public partial class InvoiceDetail : BaseEntity
    {
        [ObservableProperty]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        private int quantity;

        [ObservableProperty]
        [Range(0.01, float.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        private float price;
        [ObservableProperty] private Variant variant = new();

        // Foreign Keys
        [ObservableProperty] private Guid invoiceID;
        [ObservableProperty]
        [NotDefault(ErrorMessage ="Sản phẩm không được để trống")]
        private Guid variantId;

        [ObservableProperty] private string quantityError = string.Empty;
        [ObservableProperty] private string invoiceIDError = string.Empty;
        [ObservableProperty] private string priceError = string.Empty;

        partial void OnQuantityChanged(int newValue)
        {
            ValidateProperty(newValue, nameof(Quantity));
            QuantityError = GetFirstError(nameof(Quantity));
        }

        partial void OnInvoiceIDChanged(Guid newId)
        {
            ValidateProperty(newId, nameof(InvoiceID));
            InvoiceIDError = GetFirstError(nameof(InvoiceID));   
        }

        public bool Validate()
        {
            ValidateAllProperties();

            QuantityError = GetFirstError(nameof(Quantity));
            InvoiceIDError = GetFirstError(nameof(InvoiceID));

            return HasErrors;
        }
    }
}
