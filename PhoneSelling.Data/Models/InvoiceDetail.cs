using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;

namespace PhoneSelling.Data.Models
{
    public partial class InvoiceDetail : BaseEntity
    {
        [ObservableProperty] private int quantity;
        [ObservableProperty] private float price;
        [ObservableProperty] private Variant variant = new();

        // Foreign Keys
        [ObservableProperty] private Guid invoiceId;
        [ObservableProperty] private Guid variantId;

    }
}
