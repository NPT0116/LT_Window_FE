using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;

namespace PhoneSelling.ViewModel.Pages.Inventory
{
    public class VariantsDetailPageViewModel : BasePageViewModel
    {
        public PaginationQueryViewModel<Item> QueryViewModel { get; set; }
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

        public ItemViewModel ItemViewModel { get; set; }
        private readonly IItemRepository _itemRepository;
        public VariantsDetailPageViewModel() : base()
        {
            Debug.WriteLine("This is Variant Detail Page !");
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            QueryViewModel = new(LoadDataAsync);
            QueryViewModel.LoadDataCommand.Execute(null);
            IsGridView = false;
        }

        private async Task<IEnumerable<Item>> LoadDataAsync()
        {
            var data = await _itemRepository.GetAll();
            Debug.WriteLine(data.Count());
            return data;
        }
    }
}
