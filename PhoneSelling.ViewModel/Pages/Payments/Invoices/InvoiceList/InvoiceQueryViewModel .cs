using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceList
{
    public partial class InvoiceQueryViewModel : PaginationQueryViewModel<Invoice, InvoiceQueryParameter>
    {
        public InvoiceQueryViewModel(Func<InvoiceQueryParameter, Task<PaginationResult<Invoice>>> fetchDataFunc) : base(fetchDataFunc)
        {
        }
    }
}
