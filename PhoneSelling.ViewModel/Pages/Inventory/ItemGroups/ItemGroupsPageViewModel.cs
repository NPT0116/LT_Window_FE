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
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.VariantRepository;

namespace PhoneSelling.ViewModel.Pages.Inventory.ItemGroups
{
    public partial class ItemGroupsPageViewModel : BasePageViewModel
    {
        //public List<ItemGroup> data { get; set; } = new();
        public ObservableCollection<ItemGroup> Data { get; set; } = new();
        [ObservableProperty] private ItemGroup selectedItemGroup = new ItemGroup();
        [ObservableProperty] private Item selectedItem = new Item();
        [ObservableProperty] private bool isGridView;
        // queryviemodel:pagnination queryviewmodel
        // IItemGroupRepository
        // public ItemGroupsQueryViewModel QueryViewModel { get; set; }
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IVariantRepository _variantRepositoty;
        public ItemGroupsPageViewModel()
        {
            _itemGroupRepository = DIContainer.GetKeyedSingleton<IItemGroupRepository>();
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            _variantRepositoty = DIContainer.GetKeyedSingleton<IVariantRepository>();
            _ = LoadDataAsync();
            //Data = ItemGroupsMockData.CreatePhoneMockData();
        }
        public async Task LoadDataAsync()
        {
            //Debug.WriteLine(groupsResponse);
            var groupsResponse = await _itemGroupRepository.GetItemGroupsAsync(new ItemGroupQueryParameter());
            var groups = groupsResponse.Data;

            var items = await _itemRepository.GetAll();

            var variantsResponse = await _variantRepositoty.GetAllVariants(new VariantPaginationQuery());
            var variants = variantsResponse.Data;

            Data.Clear();

            foreach (var group in groups)
            {
                var groupItems = items.Where(item => item.ItemGroupId == group.Id).ToList();

                foreach (var item in groupItems)
                {
                    Debug.WriteLine(item.Id);
                    item.Variants = variants.Where(variant => variant.Item.Id == item.Id).ToList();
                }

                group.Items = groupItems;
                Data.Add(group);
            }
            // Debug

        }
        //private async Task<PaginationResult<ItemGroup>> LoadData(ItemGroupQueryParameter query)
        //{
        //    //IItemGroupRepository = await callapi
        //    //return IItemGroupRepository;
        //    var result = await _itemGroupRepository.GetItemGroupsAsync(query);
        //    return result;
        //}
    }

    public partial class ItemGroupsQueryViewModel : PaginationQueryViewModel<ItemGroup, ItemGroupQueryParameter>
    {
        public ItemGroupsQueryViewModel(Func<ItemGroupQueryParameter, Task<PaginationResult<ItemGroup>>> fetchDataFunc) : base(fetchDataFunc)
        {
        }
    }
}
