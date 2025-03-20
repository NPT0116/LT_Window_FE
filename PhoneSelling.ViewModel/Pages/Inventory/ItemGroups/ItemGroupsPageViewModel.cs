using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Contracts.Query;
using PhoneSelling.Data.Repositories.ItemGroupRepository;

namespace PhoneSelling.ViewModel.Pages.Inventory.ItemGroups
{
    public partial class ItemGroupsPageViewModel : BasePageViewModel
    {
        public List<ItemGroup> data { get; set; }
        [ObservableProperty]
        private ItemGroup selectedItemGroup;
        [ObservableProperty]
        private Item selectedItem;
        [ObservableProperty]
        private bool isGridView;
        // queryviemodel:pagnin queryviewmodel
        // IItemGroupRepository
        public ItemGroupsQueryViewModel QueryViewModel { get; set; }
        private readonly IItemGroupRepository _itemGroupRepository;

        public ItemGroupsPageViewModel()
        {
            data = ItemGroupsMockData.CreatePhoneMockData();
            //IItemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>()
            //QueryViewModel = new(loadData);
            QueryViewModel = new(LoadData);
            _itemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>();

        }
        
        private async Task<PaginationResult<ItemGroup>> LoadData(ItemGroupQueryParameter query)
        {
            //IItemGroupRepository = await callapi
            //return IItemGroupRepository;
            var result = await _itemGroupRepository.GetItemGroupsAsync(query);
            return result;
        }

    }

    public partial class ItemGroupsQueryViewModel : PaginationQueryViewModel<ItemGroup, ItemGroupQueryParameter>
    {
        public ItemGroupsQueryViewModel(Func<ItemGroupQueryParameter, Task<PaginationResult<ItemGroup>>> fetchDataFunc) : base(fetchDataFunc)
        {
        }
    }
}
