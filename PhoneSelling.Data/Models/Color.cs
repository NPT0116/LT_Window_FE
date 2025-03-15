using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PhoneSelling.Data.Models
{
    public partial class Color : BaseEntity
    {
        [ObservableProperty] private Guid colorId;
        [ObservableProperty] private string name;
        [ObservableProperty] private string urlImage;

        // Foreign Keys
        [ObservableProperty] private Guid itemId;
    }
}
