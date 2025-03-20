using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Text.Json.Serialization;

namespace PhoneSelling.Data.Models
{
    public partial class Manufacturer : BaseEntity
    {
        [JsonPropertyName("manufacturerID")]
        public new Guid Id { get; set; }
        [ObservableProperty] private string manufacturerName = string.Empty;
        [ObservableProperty] private string? description;
    }
}
