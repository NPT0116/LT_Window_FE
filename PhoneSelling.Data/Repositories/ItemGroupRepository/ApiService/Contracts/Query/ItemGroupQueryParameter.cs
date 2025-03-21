using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Common.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query
{
    public partial class ItemGroupQueryParameter : PaginationQuery
    {
        [ObservableProperty] private string itemGroupName = string.Empty;
    }
}
