using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PhoneSelling.Data.Models
{
    public partial class Color : BaseEntity
    {
        [ObservableProperty] private string storage;
        [ObservableProperty] private int costPrice;
        [ObservableProperty] private int sellingPrice;
        [ObservableProperty] private int stockQuantity;

        // Foreign Keys
        [ObservableProperty] private Guid variantId;
        [ObservableProperty] private Guid itemId;
    }
}
