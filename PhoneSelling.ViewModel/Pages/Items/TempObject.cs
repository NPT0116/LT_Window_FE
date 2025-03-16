using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;


namespace PhoneSelling.ViewModel.Pages.Items
{
    public partial class TempColor : ValidatableViewModel
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

        partial void OnNameChanged(string value)
        {
            ValidateName();
        }

        private void ValidateName()
        {
            ClearErrors(nameof(Name));

            if (string.IsNullOrWhiteSpace(Name))
            {
                AddError(nameof(Name), "Color name cannot be empty.");
            }
            else if (Name.Length < 3)
            {
                AddError(nameof(Name), "Color name must be at least 3 characters.");
            }
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
