using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PhoneSelling.Data.Models
{
    public partial class Manufacturer : BaseEntity
    {
        [ObservableProperty] private string manufacturerName = string.Empty;
        [ObservableProperty] private string? description;
    }
}
