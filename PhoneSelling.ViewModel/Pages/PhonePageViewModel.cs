using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System.Diagnostics;

namespace PhoneSelling.ViewModel.Pages
{
    public class PhonePageViewModel : BasePageViewModel
    {
        public PaginationQueryViewModel<Item> QueryViewModel { get; set; }
        private readonly IItemRepository _itemRepository;
        public PhonePageViewModel() : base()
        {
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
            QueryViewModel = new(LoadDataAsync);
            QueryViewModel.LoadDataCommand.Execute(null);
        }

        private async Task<IEnumerable<Item>> LoadDataAsync()
        {
            var data = await _itemRepository.GetAll();
            Debug.WriteLine(data.Count());
            return data;
        }
    }
}
