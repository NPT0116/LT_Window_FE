using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Common
{
    public partial class VariantViewModel : ObservableObject
    {
        [ObservableProperty] private string storage;
        [ObservableProperty] private string colorTempId;
        [ObservableProperty] private decimal costPrice;
        [ObservableProperty] private decimal sellingPrice;
        [ObservableProperty] private int stockQuantity;

        public VariantViewModel(string storage, int colorTempId)
        {
            Storage = storage;
            //ColorTempId = colorTempId;
        }
    }
}
