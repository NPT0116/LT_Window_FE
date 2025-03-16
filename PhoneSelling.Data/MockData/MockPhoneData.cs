using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PhoneSelling.Data.Models;

namespace PhoneSelling.Data.Repositories.ItemRepository
{
    public static class MockPhoneData
    {
        // Placeholder image for all products.
        private static readonly string placeholderImage = "https://cdn.viettelstore.vn/Images/Product/ProductImage/803717658.jpeg";

        public static List<ItemGroup> CreateAllPhoneGroups()
        {
            var groups = new List<ItemGroup>();

            // ***************** iPhone Group *****************
            var manufacturerApple = Guid.NewGuid();
            var iphoneGroup = new ItemGroup
            {
                Id = Guid.NewGuid(),
                ItemGroupName = "iPhone",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            // iPhone 13
            var iphone13 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 13",
                Description = "The standard iPhone 13 with advanced dual cameras and improved battery life.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 9, 24),
                ItemGroupId = iphoneGroup.Id,
                ManufacturerId = manufacturerApple,
                Variants = new List<Variant>()
            };
            iphone13.Variants.AddRange(new[]
            {
                CreateVariant(iphone13.Id, 128, 700, 799, 100, "Blue"),
                CreateVariant(iphone13.Id, 256, 750, 899, 50, "Blue"),
                // Additional variant for iPhone 13
                CreateVariant(iphone13.Id, 512, 800, 999, 30, "Blue")
            });

            // iPhone 13 Plus
            var iphone13Plus = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 13 Plus",
                Description = "The larger iPhone 13 Plus with enhanced battery performance.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 9, 24),
                ItemGroupId = iphoneGroup.Id,
                ManufacturerId = manufacturerApple,
                Variants = new List<Variant>()
            };
            iphone13Plus.Variants.AddRange(new[]
            {
                CreateVariant(iphone13Plus.Id, 128, 750, 849, 80, "Green"),
                CreateVariant(iphone13Plus.Id, 256, 800, 949, 40, "Green"),
                // Additional variant for iPhone 13 Plus
                CreateVariant(iphone13Plus.Id, 512, 850, 1049, 20, "Green")
            });

            // iPhone 14
            var iphone14 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 14",
                Description = "The iPhone 14 introduces subtle design refinements and improved efficiency.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2022, 9, 14),
                ItemGroupId = iphoneGroup.Id,
                ManufacturerId = manufacturerApple,
                Variants = new List<Variant>()
            };
            iphone14.Variants.AddRange(new[]
            {
                CreateVariant(iphone14.Id, 128, 750, 849, 120, "Silver"),
                CreateVariant(iphone14.Id, 256, 800, 949, 80, "Silver")
            });

            // iPhone 14 Pro
            var iphone14Pro = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 14 Pro",
                Description = "The iPhone 14 Pro features a new dynamic island and an always-on display.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2022, 9, 14),
                ItemGroupId = iphoneGroup.Id,
                ManufacturerId = manufacturerApple,
                Variants = new List<Variant>()
            };
            iphone14Pro.Variants.AddRange(new[]
            {
                CreateVariant(iphone14Pro.Id, 128, 800, 899, 100, "Gold"),
                CreateVariant(iphone14Pro.Id, 256, 850, 999, 70, "Gold"),
                CreateVariant(iphone14Pro.Id, 512, 900, 1099, 40, "Gold")
            });

            iphoneGroup.Items.AddRange(new[] { iphone13, iphone13Plus, iphone14, iphone14Pro });
            groups.Add(iphoneGroup);

            // ***************** Samsung Group *****************
            var manufacturerSamsung = Guid.NewGuid();
            var samsungGroup = new ItemGroup
            {
                Id = Guid.NewGuid(),
                ItemGroupName = "Samsung Galaxy",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            // Samsung Galaxy S21
            var galaxyS21 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Samsung Galaxy S21",
                Description = "The Galaxy S21 offers cutting-edge performance and a pro-grade camera.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 1, 29),
                ItemGroupId = samsungGroup.Id,
                ManufacturerId = manufacturerSamsung,
                Variants = new List<Variant>()
            };
            galaxyS21.Variants.AddRange(new[]
            {
                CreateVariant(galaxyS21.Id, 128, 650, 850, 150, "Black"),
                CreateVariant(galaxyS21.Id, 256, 700, 950, 100, "Black")
            });

            // Samsung Galaxy S21 Ultra
            var galaxyS21Ultra = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Samsung Galaxy S21 Ultra",
                Description = "The Galaxy S21 Ultra is the premium model with an advanced camera system.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 1, 29),
                ItemGroupId = samsungGroup.Id,
                ManufacturerId = manufacturerSamsung,
                Variants = new List<Variant>()
            };
            galaxyS21Ultra.Variants.AddRange(new[]
            {
                CreateVariant(galaxyS21Ultra.Id, 256, 900, 1200, 60, "Black"),
                CreateVariant(galaxyS21Ultra.Id, 512, 950, 1300, 30, "Black")
            });

            // Samsung Galaxy S22
            var galaxyS22 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Samsung Galaxy S22",
                Description = "The Galaxy S22 features a sleek design and powerful performance.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2022, 2, 10),
                ItemGroupId = samsungGroup.Id,
                ManufacturerId = manufacturerSamsung,
                Variants = new List<Variant>()
            };
            galaxyS22.Variants.AddRange(new[]
            {
                CreateVariant(galaxyS22.Id, 128, 700, 900, 140, "White"),
                CreateVariant(galaxyS22.Id, 256, 750, 1000, 80, "White")
            });

            samsungGroup.Items.AddRange(new[] { galaxyS21, galaxyS21Ultra, galaxyS22 });
            groups.Add(samsungGroup);

            // ***************** Xiaomi Group *****************
            var manufacturerXiaomi = Guid.NewGuid();
            var xiaomiGroup = new ItemGroup
            {
                Id = Guid.NewGuid(),
                ItemGroupName = "Xiaomi Mi",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            // Xiaomi Mi 11
            var mi11 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Xiaomi Mi 11",
                Description = "The Xiaomi Mi 11 offers flagship specs at a competitive price.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 1, 1),
                ItemGroupId = xiaomiGroup.Id,
                ManufacturerId = manufacturerXiaomi,
                Variants = new List<Variant>()
            };
            mi11.Variants.AddRange(new[]
            {
                CreateVariant(mi11.Id, 128, 600, 749, 130, "Blue"),
                CreateVariant(mi11.Id, 256, 650, 799, 70, "Blue")
            });

            // Xiaomi Redmi Note 10
            var redmiNote10 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Xiaomi Redmi Note 10",
                Description = "The Redmi Note 10 offers great performance and a vibrant display.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 3, 15),
                ItemGroupId = xiaomiGroup.Id,
                ManufacturerId = manufacturerXiaomi,
                Variants = new List<Variant>()
            };
            redmiNote10.Variants.AddRange(new[]
            {
                CreateVariant(redmiNote10.Id, 64, 200, 249, 180, "Red"),
                CreateVariant(redmiNote10.Id, 128, 250, 299, 150, "Red"),
                // Additional variant for Redmi Note 10
                CreateVariant(redmiNote10.Id, 256, 300, 349, 100, "Red")
            });

            xiaomiGroup.Items.AddRange(new[] { mi11, redmiNote10 });
            groups.Add(xiaomiGroup);

            // ***************** Huawei Group *****************
            var manufacturerHuawei = Guid.NewGuid();
            var huaweiGroup = new ItemGroup
            {
                Id = Guid.NewGuid(),
                ItemGroupName = "Huawei Mate",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            // Huawei Mate 40
            var mate40 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Huawei Mate 40",
                Description = "The Huawei Mate 40 features an innovative camera system and sleek design.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2020, 10, 22),
                ItemGroupId = huaweiGroup.Id,
                ManufacturerId = manufacturerHuawei,
                Variants = new List<Variant>()
            };
            mate40.Variants.AddRange(new[]
            {
                CreateVariant(mate40.Id, 128, 700, 799, 90, "Black"),
                CreateVariant(mate40.Id, 256, 750, 899, 60, "Black")
            });

            // Huawei P40 Pro
            var p40Pro = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Huawei P40 Pro",
                Description = "The Huawei P40 Pro offers advanced photography features in a sleek design.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2020, 4, 7),
                ItemGroupId = huaweiGroup.Id,
                ManufacturerId = manufacturerHuawei,
                Variants = new List<Variant>()
            };
            p40Pro.Variants.AddRange(new[]
            {
                CreateVariant(p40Pro.Id, 128, 650, 749, 100, "Silver"),
                CreateVariant(p40Pro.Id, 256, 700, 849, 80, "Silver")
            });

            huaweiGroup.Items.AddRange(new[] { mate40, p40Pro });
            groups.Add(huaweiGroup);

            // ***************** BONUS: OnePlus Group *****************
            var manufacturerOnePlus = Guid.NewGuid();
            var onePlusGroup = new ItemGroup
            {
                Id = Guid.NewGuid(),
                ItemGroupName = "OnePlus",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            // OnePlus 9
            var onePlus9 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "OnePlus 9",
                Description = "OnePlus 9 offers a smooth experience and fast charging.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 3, 23),
                ItemGroupId = onePlusGroup.Id,
                ManufacturerId = manufacturerOnePlus,
                Variants = new List<Variant>()
            };
            onePlus9.Variants.AddRange(new[]
            {
                CreateVariant(onePlus9.Id, 128, 720, 799, 120, "Black"),
                CreateVariant(onePlus9.Id, 256, 770, 849, 80, "Black")
            });

            // OnePlus 9 Pro
            var onePlus9Pro = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "OnePlus 9 Pro",
                Description = "OnePlus 9 Pro features a high-resolution display and premium camera system.",
                Picture = placeholderImage,
                ReleasedDate = new DateTime(2021, 3, 23),
                ItemGroupId = onePlusGroup.Id,
                ManufacturerId = manufacturerOnePlus,
                Variants = new List<Variant>()
            };
            onePlus9Pro.Variants.AddRange(new[]
            {
                CreateVariant(onePlus9Pro.Id, 128, 800, 899, 100, "Blue"),
                CreateVariant(onePlus9Pro.Id, 256, 850, 949, 60, "Blue"),
                CreateVariant(onePlus9Pro.Id, 512, 900, 1049, 40, "Blue")
            });

            onePlusGroup.Items.AddRange(new[] { onePlus9, onePlus9Pro });
            groups.Add(onePlusGroup);

            return groups;
        }

        /// <summary>
        /// Creates a Variant instance and sets all its properties, including Color via reflection and Inventory details.
        /// </summary>
        /// <param name="itemId">The parent item ID.</param>
        /// <param name="storage">Storage capacity.</param>
        /// <param name="costPrice">Cost price.</param>
        /// <param name="sellingPrice">Selling price.</param>
        /// <param name="stockQuantity">Stock quantity.</param>
        /// <param name="color">Color to assign.</param>
        /// <returns>A new Variant instance with the specified properties.</returns>
        private static Variant CreateVariant(Guid itemId, int storage, float costPrice, float sellingPrice, int stockQuantity, string color)
        {
            var variant = new Variant
            {
                Id = Guid.NewGuid(),
                Storage = storage,
                CostPrice = costPrice,
                SellingPrice = sellingPrice,
                StockQuantity = stockQuantity,
                ItemId = itemId,
                // Create Inventory instance with matching stock, last updated as now, and will assign VariantId below.
                Inventory = new Inventory
                {
                    StockQuantity = stockQuantity,
                    LastUpdated = DateTime.Now
                }
            };

            // Now assign the inventory's VariantId to this variant's Id.
            variant.Inventory.VariantId = variant.Id;

            // Set the read-only Color property using reflection.
            var colorField = typeof(Variant).GetField("<Color>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            if (colorField != null)
            {
                colorField.SetValue(variant, color);
            }

            return variant;
        }
    }
}
