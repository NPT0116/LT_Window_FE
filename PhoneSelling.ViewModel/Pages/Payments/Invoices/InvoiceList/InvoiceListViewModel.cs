using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InvoiceRepository;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Query;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;

using PhoneSelling.ViewModel.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceList
{
    public partial class InvoiceListViewModel : BasePageViewModel
    {
        public InvoiceQueryViewModel InvoiceQuery { get; set; }
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceListViewModel() : base()
        {
            _invoiceRepository = DIContainer.GetKeyedSingleton<IInvoiceRepository>();
            InvoiceQuery = new(LoadDataAsync);
            InvoiceQuery.LoadDataCommand.Execute(null);
        }

        private async Task<PaginationResult<Invoice>> LoadDataAsync(InvoiceQueryParameter query)
        {
            var invoices = await _invoiceRepository.GetAllInvoices(new InvoiceQueryParameter { PageSize = 5});
            return invoices;
        }
    }
}
