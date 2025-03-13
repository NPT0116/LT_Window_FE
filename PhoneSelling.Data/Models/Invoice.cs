using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PhoneSelling.Data.Models
{
    public partial class Invoice : BaseEntity
    {
        [ObservableProperty] private float totalAmount;
        [ObservableProperty] private DateTime date;

        // Foreign Key
        [ObservableProperty] private Guid customerId;
    }
}
