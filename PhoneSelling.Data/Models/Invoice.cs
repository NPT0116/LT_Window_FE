using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace PhoneSelling.Data.Models
{
    public partial class Invoice : BaseEntity
    {
        [ObservableProperty] private float totalAmount;
        [ObservableProperty] private DateTime date;
        [ObservableProperty] private ObservableCollection<InvoiceDetail> invoiceDetails;


        // Foreign Key
        [ObservableProperty] private Guid customerId;
    }
}
