using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Common;
using System;
using System.Collections.ObjectModel;

namespace PhoneSelling.Data.Models
{
    public partial class Invoice : BaseEntity
    {
        [ObservableProperty] private Guid invoiceID;
        [ObservableProperty] private float totalAmount;
        [ObservableProperty] private DateTime date;
        [ObservableProperty] private TrulyObservableCollection<InvoiceDetail> _invoiceDetails = new();
        // Foreign Key
        [ObservableProperty] private Guid customerID;
    }
}
