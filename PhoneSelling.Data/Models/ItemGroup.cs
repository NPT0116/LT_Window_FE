using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace PhoneSelling.Data.Models
{
    public partial class ItemGroup : BaseEntity
    {
        [ObservableProperty] private string itemGroupName = string.Empty;
        [ObservableProperty] private string category = string.Empty;
        [ObservableProperty] private List<Item> items = new();
    }
}
