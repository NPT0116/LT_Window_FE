using CommunityToolkit.Mvvm.ComponentModel;
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
        [ObservableProperty] private Guid variantId;

        [ObservableProperty] private string quantityError = string.Empty;
        [ObservableProperty] private string priceError = string.Empty;


    }
}
