using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PhoneSelling.Data.Models
{
    public partial class Inventory : BaseEntity
    {
        [ObservableProperty] private int stockQuantity;
        [ObservableProperty] private DateTime lastUpdated;

        // Foreign Key
        [ObservableProperty] private Guid variantId;
    }
}
