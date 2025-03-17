using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Common.Contracts.Requests
{
    public enum SortDirection
    {
        [EnumMember(Value = "asc")]
        Ascending,

        [EnumMember(Value = "desc")]
        Descending
    }
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchKey { get; set; } = string.Empty;
        public string Sortby { get; set; } = string.Empty;
        public SortDirection SortDirection { get; set; }
    }
}
