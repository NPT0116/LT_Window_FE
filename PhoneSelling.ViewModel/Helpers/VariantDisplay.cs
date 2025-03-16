using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneSelling.Data.Models;

namespace PhoneSelling.ViewModel.Helpers
{
    public class VariantDisplay
    {
        public string ParentItemName { get; set; }
        public Variant Variant { get; set; }

        // For convenience, provide a computed property for display
        public string DisplayName => $"{ParentItemName} - {Variant.Storage}GB, {Variant.SellingPrice} USD, Stock: {Variant.StockQuantity}";
    }

}
