using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using PhoneSelling.Data.Models;

namespace Navigation.Converters
{
    public class VariantNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Variant variant)
            {
                // Get the parent item's name, the storage, and the color name.
                // Assuming 'variant.Item' is set, and 'variant.Storage' is a string.
                string itemName = variant.Item != null ? variant.Item.ItemName : "";
                string storage = variant.Storage;
                string colorName = "";

                if (variant.Color != null)
                {
                    // If the Color class has a "Name" property, use it.
                    var prop = variant.Color.GetType().GetProperty("Name");
                    if (prop != null)
                    {
                        colorName = prop.GetValue(variant.Color)?.ToString() ?? "";
                    }
                    else
                    {
                        colorName = variant.Color.ToString();
                    }
                }

                return $"{itemName} - {storage} - {colorName}";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
