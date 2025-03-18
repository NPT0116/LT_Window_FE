using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PhoneSelling.Data.Models
{
    public partial class Variant : BaseEntity
    {
        [ObservableProperty] private Guid variantID;
        [ObservableProperty] private Item item;
        [ObservableProperty] private Color color;
        [ObservableProperty] private string storage;
        [ObservableProperty] private float costPrice;
        [ObservableProperty] private float sellingPrice;
        [ObservableProperty] private int stockQuantity;

        // Foreign Key
        [ObservableProperty] private Guid itemId;
        [ObservableProperty] private Guid colorId;
    }
}
