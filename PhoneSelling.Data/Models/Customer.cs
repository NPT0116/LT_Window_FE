using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace PhoneSelling.Data.Models
{
    public partial class Customer : BaseEntity
    {
        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string email = string.Empty;
        [ObservableProperty] private string phone = string.Empty;
        [ObservableProperty] private string address = string.Empty;
        [ObservableProperty] private List<Invoice> invoices = new();
    }
}
