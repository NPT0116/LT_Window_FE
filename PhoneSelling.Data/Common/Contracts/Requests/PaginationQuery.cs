using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    public partial class PaginationQuery : ObservableObject
    {
        [ObservableProperty] private int pageNumber = 1;
        [ObservableProperty] private int pageSize = 10;
        [ObservableProperty] string searchKey = string.Empty;
        [ObservableProperty] string sortby = string.Empty;
        [ObservableProperty] SortDirection sortDirection;
        

        public int SortDirectionIndex
        {
            get => SortDirection == SortDirection.Ascending ? 0 : 1;
            set => SortDirection = (value == 0) ? SortDirection.Ascending : SortDirection.Descending;
        }

        protected void ResetPagination()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
