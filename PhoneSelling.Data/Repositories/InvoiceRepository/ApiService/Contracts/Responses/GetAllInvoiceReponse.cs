using PhoneSelling.Data.Common.Contracts.Responses;
using PhoneSelling.Data.Contracts.Responses.Base;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InvoiceRepository.ApiService.Contracts.Responses
{
    public class GetAllInvoiceReponse : PaginationApiResponse<InvoiceDto>
    {
    }
}
