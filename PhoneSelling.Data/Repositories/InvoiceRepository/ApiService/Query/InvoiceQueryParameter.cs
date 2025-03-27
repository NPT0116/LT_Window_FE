using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Common.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query
{
    public partial class InvoiceQueryParameter : PaginationQuery
    {
        [ObservableProperty] private InvoiceDatetimeQueryParameter invoiceDatetimeQueryParameter = new();
        [ObservableProperty] private string? customerName;
        [ObservableProperty] private string? customerPhone;
    }

    public partial class InvoiceDatetimeQueryParameter : ObservableObject
    {
        [ObservableProperty] private DateTime? fromDate = null;
        [ObservableProperty] private DateTime? toDate = null;
        [ObservableProperty] private string sortDirection = "desc";
    }

}