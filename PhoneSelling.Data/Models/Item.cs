using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public partial class Item : BaseEntity
    {
        [ObservableProperty] private string itemName = string.Empty;
        [ObservableProperty] private string? description;
        [ObservableProperty] private string? picture;
        [ObservableProperty] private DateTime releasedDate = DateTime.UtcNow;
        [ObservableProperty] private List<Variant> variants = new();

        // Foreign Keys
        [ObservableProperty] private Guid itemGroupId;
        [ObservableProperty] private Guid manufacturerId;

    }
}
