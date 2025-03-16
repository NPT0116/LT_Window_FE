using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace PhoneSelling.ViewModel.Pages.Items
{
    public partial class TempColor : ObservableObject
    {
        public static int CURRENT_TEMP_ID = 1;
        [ObservableProperty] private int tempId;
        [ObservableProperty] private string name;
        [ObservableProperty] private string urlImage;
        [ObservableProperty] private Guid colorId;

        [RelayCommand]
        private void UpdateColorName(string name)
        {
            Name = name;
        }

        
    }

    public partial class TempVariant : ObservableObject
    {
        [ObservableProperty] private string storage;
        [ObservableProperty] private int costPrice;
        [ObservableProperty] private int sellingPrice;
        [ObservableProperty] private int stockQuantity;
        [ObservableProperty] private int colorTempId;
    }
}
