using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PhoneSelling.Data.Models
{
    public partial class Variant : BaseEntity
    {
        [ObservableProperty] private string color = string.Empty;
        [ObservableProperty] private int storage;
        [ObservableProperty] private float costPrice;
        [ObservableProperty] private float sellingPrice;
        [ObservableProperty] private int stockQuantity;
        [ObservableProperty] private Inventory inventory = new();

        // Foreign Key
        [ObservableProperty] private Guid itemId;
    }
}
