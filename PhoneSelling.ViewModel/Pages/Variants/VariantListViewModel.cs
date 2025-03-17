using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Variants
{
    public partial class VariantListViewModel : BasePageViewModel
    {
        public VariantQueryViewModel QueryViewModel { get; set; }
        private readonly IVariantRepository _variantRepository;
        public VariantListViewModel()
        {
            QueryViewModel = new(LoadDataAsync);
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();
        }

        private async Task<PaginationResult<Variant>> LoadDataAsync(VariantPaginationQuery query)
        {
            var paginationResult = await _variantRepository.GetAllVariants(query);
            return paginationResult;
        }
    }
}
