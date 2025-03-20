using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PhoneSelling.Data.Models
{
    public partial class ItemGroup : BaseEntity
    {
        [JsonPropertyName("itemGroupID")]
        public new Guid Id { get; set; }
        [ObservableProperty] private string itemGroupName = string.Empty;
        [ObservableProperty] private string category = string.Empty;
        [ObservableProperty] private List<Item> items = new();
    }
}
