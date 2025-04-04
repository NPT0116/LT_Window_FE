﻿using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Variants
{
    public class SortOption
    {
        public string DisplayName { get; set; } // Shown in UI
        public string ActualValue { get; set; } // Used internally
    }

    public partial class VariantQueryViewModel : PaginationQueryViewModel<Variant, VariantPaginationQuery>
    {
        public VariantQueryViewModel(Func<VariantPaginationQuery, Task<PaginationResult<Variant>>> fetchDataFunc) : base(fetchDataFunc)
        {
        }
        public ObservableCollection<SortOption> SortOptions { get; } = new()
        {
            new SortOption { DisplayName = "Giá nhập", ActualValue = "costPrice" },
            new SortOption { DisplayName = "Giá bán", ActualValue = "sellingPrice" },
            new SortOption { DisplayName = "Tồn kho", ActualValue = "stockQuantity" },
        };

    }
}
