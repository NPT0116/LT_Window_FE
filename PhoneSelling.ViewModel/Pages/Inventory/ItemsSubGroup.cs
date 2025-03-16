using PhoneSelling.Data.Models;
using System.Collections.ObjectModel;

namespace PhoneSelling.ViewModel.Pages.Inventory
{
    public class ItemsSubGroup
    {
        public string GroupName { get; set; }
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    }
}
