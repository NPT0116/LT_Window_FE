using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Pages.Variants;

namespace PhoneSelling.ViewModel.Pages.Inventory.Variants
{
    public class VariantsListPageViewModel : BasePageViewModel
    {
        public VariantQueryViewModel QueryViewModel { get; set; }
        public ItemViewModel ItemViewModel { get; set; }
        private readonly IVariantRepository _variantRepository;
        private readonly IItemRepository _itemRepository;
        private bool _isGridView;
        public bool IsGridView
        {
            get => _isGridView;
            set
            {
                Debug.WriteLine(value);
                if (_isGridView != value)
                {
                    _isGridView = value;
                    OnPropertyChanged(nameof(IsGridView));
                }
            }
        }


        public VariantsListPageViewModel() : base()
        {
            Debug.WriteLine("This is Variants List Page !");
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();
            QueryViewModel = new(LoadDataAsync);
            QueryViewModel.LoadDataCommand.Execute(null);
            IsGridView = false;
        }

        private async Task<PaginationResult<Variant>> LoadDataAsync(VariantPaginationQuery query)
        {
            var paginationResult = await _variantRepository.GetAllVariants(query);
            return paginationResult;
        }
    }
}
