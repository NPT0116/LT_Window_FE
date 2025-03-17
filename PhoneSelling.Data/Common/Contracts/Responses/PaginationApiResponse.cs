using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Common.Contracts.Responses
{
    public class PaginationApiResponse<T>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string firstPage { get; set; }
        public string lastPage { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }
        public string nextPage { get; set; }
        public string previousPage { get; set; }
        public List<T> data { get; set; }
        public bool succeeded { get; set; }
        public List<string>? errors { get; set; }
        public string? message { get; set; }
    }
}
