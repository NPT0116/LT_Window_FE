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
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        private int quantity = 1;

        [ObservableProperty]
        [Range(0.01, float.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        private float price = 0;

        [ObservableProperty] private Variant variant = new();

        public float Total => quantity * price;

        // Foreign Keys
        [ObservableProperty] private Guid invoiceID;
        [ObservableProperty]
        [NotDefault(ErrorMessage ="Sản phẩm không được để trống")]
        private Guid variantId = new Guid();

        [ObservableProperty] private string quantityError = string.Empty;
        [ObservableProperty] private string variantIDError = string.Empty;

        [ObservableProperty] private string invoiceIDError = string.Empty;
        [ObservableProperty] private string priceError = string.Empty;
        public Action? RecalculateCallback { get; set; }

        partial void OnPriceChanged(float newValue)
        {
            ValidateProperty(newValue, nameof(Price));
            RecalculateCallback?.Invoke();
            OnPropertyChanged(nameof(Total));
        }
        partial void OnVariantChanged(Variant newValue)
        {
            ValidateProperty(newValue, nameof(Variant));
            RecalculateCallback?.Invoke();
            OnPropertyChanged(nameof(Total));
        }

        partial void OnQuantityChanged(int newValue)
        {
            ValidateProperty(newValue, nameof(Quantity));
            QuantityError = GetFirstError(nameof(Quantity));
            RecalculateCallback?.Invoke();
            OnPropertyChanged(nameof(Total));
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
            Debug.WriteLine(QuantityError);
            InvoiceIDError = GetFirstError(nameof(InvoiceID));
            Debug.WriteLine(InvoiceIDError);
            VariantIDError = GetFirstError(nameof(VariantId));
            Debug.WriteLine(VariantIDError);
            if (!string.IsNullOrEmpty(VariantIDError))
            {
                OnPropertyChanged(nameof(VariantIDError));
            }


            return HasErrors;
        }
    }
}
