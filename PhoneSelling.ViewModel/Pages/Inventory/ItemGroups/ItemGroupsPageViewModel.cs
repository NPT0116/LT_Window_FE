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

        public ItemGroupsPageViewModel()
        {
            data = ItemGroupsMockData.CreatePhoneMockData();
            //IItemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>()
            //QueryViewModel = new(loadData);

        }
        //private async Task<PaginationResult<ItemGroup>> loadData(ItemGroupPaginationQuery)
        //{
        //    //IItemGroupRepository = await callapi
        //    //return IItemGroupRepository;
        //}

    }
}
