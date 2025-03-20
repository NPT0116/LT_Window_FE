using PhoneSelling.Data.Models;

namespace PhoneSelling.ViewModel.Pages.Inventory.ItemGroups
{
    public class ItemGroupsMockData
    {
        public static List<ItemGroup> CreatePhoneMockData()
        {
            var groups = new List<ItemGroup>();

            // ---------------------------------
            // iPhone 13 Series Group
            // ---------------------------------
            Guid iphone13SeriesGroupId = Guid.NewGuid();
            var iphone13Series = new ItemGroup
            {
                ItemGroupName = "iPhone 13 Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            // iPhone 13 item
            var iphone13 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 13",
                Description = "The iPhone 13 offers advanced dual cameras and improved battery life.",
                Picture = "https://example.com/iphone13.jpg",
                ReleaseDate = new DateTime(2021, 9, 24),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = iphone13SeriesGroupId,
                Variants = new List<Variant>()
            };

            // Colors with UrlImage added
            var blueColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Blue",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };
            var silverColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Silver",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };

            // Variants for iPhone 13
            var iphone13Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone13,
                Storage = "128",
                CostPrice = 700,
                SellingPrice = 799,
                StockQuantity = 100,
                ItemId = iphone13.Id,
                Color = blueColor
            };
            var iphone13Variant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone13,
                Storage = "256",
                CostPrice = 750,
                SellingPrice = 899,
                StockQuantity = 50,
                ItemId = iphone13.Id,
                Color = silverColor
            };
            iphone13.Variants.Add(iphone13Variant1);
            iphone13.Variants.Add(iphone13Variant2);
            iphone13Series.Items.Add(iphone13);
            groups.Add(iphone13Series);

            // ---------------------------------
            // iPhone 14 Series Group
            // ---------------------------------
            Guid iphone14SeriesGroupId = Guid.NewGuid();
            var iphone14Series = new ItemGroup
            {
                ItemGroupName = "iPhone 14 Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var iphone14 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 14",
                Description = "The iPhone 14 introduces subtle design refinements and improved efficiency.",
                Picture = "https://example.com/iphone14.jpg",
                ReleaseDate = new DateTime(2022, 9, 14),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = iphone14SeriesGroupId,
                Variants = new List<Variant>()
            };

            var graphiteColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Graphite",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };
            var goldColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Gold",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };

            var iphone14Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone14,
                Storage = "128",
                CostPrice = 750,
                SellingPrice = 849,
                StockQuantity = 120,
                ItemId = iphone14.Id,
                Color = graphiteColor
            };
            var iphone14Variant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone14,
                Storage = "256",
                CostPrice = 800,
                SellingPrice = 949,
                StockQuantity = 80,
                ItemId = iphone14.Id,
                Color = goldColor
            };
            iphone14.Variants.Add(iphone14Variant1);
            iphone14.Variants.Add(iphone14Variant2);
            iphone14Series.Items.Add(iphone14);
            groups.Add(iphone14Series);

            // ---------------------------------
            // iPhone 15 Series Group
            // ---------------------------------
            Guid iphone15SeriesGroupId = Guid.NewGuid();
            var iphone15Series = new ItemGroup
            {
                ItemGroupName = "iPhone 15 Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var iphone15 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 15",
                Description = "The iPhone 15 introduces groundbreaking features and enhanced durability.",
                Picture = "https://example.com/iphone15.jpg",
                ReleaseDate = new DateTime(2023, 9, 12),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = iphone15SeriesGroupId,
                Variants = new List<Variant>()
            };

            var midnightColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Midnight",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };
            var starlightColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Starlight",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };

            var iphone15Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone15,
                Storage = "128",
                CostPrice = 850,
                SellingPrice = 949,
                StockQuantity = 110,
                ItemId = iphone15.Id,
                Color = midnightColor
            };
            var iphone15Variant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone15,
                Storage = "256",
                CostPrice = 900,
                SellingPrice = 1049,
                StockQuantity = 70,
                ItemId = iphone15.Id,
                Color = starlightColor
            };
            iphone15.Variants.Add(iphone15Variant1);
            iphone15.Variants.Add(iphone15Variant2);
            iphone15Series.Items.Add(iphone15);
            groups.Add(iphone15Series);

            // ---------------------------------
            // iPhone 16 Series Group
            // ---------------------------------
            Guid iphone16SeriesGroupId = Guid.NewGuid();
            var iphone16Series = new ItemGroup
            {
                ItemGroupName = "iPhone 16 Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var iphone16 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "iPhone 16",
                Description = "The iPhone 16 pushes boundaries with its innovative design and powerful features.",
                Picture = "https://example.com/iphone16.jpg",
                ReleaseDate = new DateTime(2024, 9, 10),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = iphone16SeriesGroupId,
                Variants = new List<Variant>()
            };

            var spaceBlackColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Space Black",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };
            var sunsetGoldColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Sunset Gold",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };

            var iphone16Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone16,
                Storage = "128",
                CostPrice = 900,
                SellingPrice = 999,
                StockQuantity = 80,
                ItemId = iphone16.Id,
                Color = spaceBlackColor
            };
            var iphone16Variant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = iphone16,
                Storage = "256",
                CostPrice = 950,
                SellingPrice = 1099,
                StockQuantity = 60,
                ItemId = iphone16.Id,
                Color = sunsetGoldColor
            };
            iphone16.Variants.Add(iphone16Variant1);
            iphone16.Variants.Add(iphone16Variant2);
            iphone16Series.Items.Add(iphone16);
            groups.Add(iphone16Series);

            // ---------------------------------
            // Xiaomi Mi Series Group
            // ---------------------------------
            Guid xiaomiGroupId = Guid.NewGuid();
            var xiaomiGroup = new ItemGroup
            {
                ItemGroupName = "Xiaomi Mi Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var mi11 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Xiaomi Mi 11",
                Description = "The Xiaomi Mi 11 offers flagship specs at a competitive price.",
                Picture = "https://example.com/mi11.jpg",
                ReleaseDate = new DateTime(2021, 1, 1),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = xiaomiGroupId,
                Variants = new List<Variant>()
            };

            var redColor = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Red",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };
            // Reusing blueColor variable name for Xiaomi Mi 11 variant (could be renamed if needed)
            var blueColorForMi11 = new Color
            {
                Id = Guid.NewGuid(),
                Name = "Blue",
                UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
            };

            var mi11Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = mi11,
                Storage = "128",
                CostPrice = 600,
                SellingPrice = 749,
                StockQuantity = 130,
                ItemId = mi11.Id,
                Color = redColor
            };
            var mi11Variant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = mi11,
                Storage = "256",
                CostPrice = 650,
                SellingPrice = 799,
                StockQuantity = 70,
                ItemId = mi11.Id,
                Color = blueColorForMi11
            };
            mi11.Variants.Add(mi11Variant1);
            mi11.Variants.Add(mi11Variant2);
            xiaomiGroup.Items.Add(mi11);
            groups.Add(xiaomiGroup);

            // ---------------------------------
            // Huawei P Series Group
            // ---------------------------------
            Guid huaweiGroupId = Guid.NewGuid();
            var huaweiGroup = new ItemGroup
            {
                ItemGroupName = "Huawei P Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var p40Pro = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Huawei P40 Pro",
                Description = "The Huawei P40 Pro offers advanced photography features in a sleek design.",
                Picture = "https://example.com/p40pro.jpg",
                ReleaseDate = new DateTime(2020, 4, 7),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = huaweiGroupId,
                Variants = new List<Variant>()
            };

            var p40ProVariant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = p40Pro,
                Storage = "128",
                CostPrice = 650,
                SellingPrice = 749,
                StockQuantity = 100,
                ItemId = p40Pro.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Black",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };
            var p40ProVariant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = p40Pro,
                Storage = "256",
                CostPrice = 700,
                SellingPrice = 849,
                StockQuantity = 80,
                ItemId = p40Pro.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Silver",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };
            p40Pro.Variants.Add(p40ProVariant1);
            p40Pro.Variants.Add(p40ProVariant2);
            huaweiGroup.Items.Add(p40Pro);
            groups.Add(huaweiGroup);

            // ---------------------------------
            // Oppo Reno Series Group
            // ---------------------------------
            Guid oppoGroupId = Guid.NewGuid();
            var oppoGroup = new ItemGroup
            {
                ItemGroupName = "Oppo Reno Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var reno5 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Oppo Reno5",
                Description = "Oppo Reno5 offers a great balance of performance and style.",
                Picture = "https://example.com/reno5.jpg",
                ReleaseDate = new DateTime(2021, 3, 15),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = oppoGroupId,
                Variants = new List<Variant>()
            };

            var reno5Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = reno5,
                Storage = "128",
                CostPrice = 400,
                SellingPrice = 499,
                StockQuantity = 120,
                ItemId = reno5.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Purple",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            reno5.Variants.Add(reno5Variant1);
            oppoGroup.Items.Add(reno5);
            groups.Add(oppoGroup);

            // ---------------------------------
            // Vivo X Series Group
            // ---------------------------------
            Guid vivoGroupId = Guid.NewGuid();
            var vivoGroup = new ItemGroup
            {
                ItemGroupName = "Vivo X Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var vivoX60 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Vivo X60",
                Description = "Vivo X60 features high-end photography and smooth performance.",
                Picture = "https://example.com/x60.jpg",
                ReleaseDate = new DateTime(2021, 8, 1),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = vivoGroupId,
                Variants = new List<Variant>()
            };

            var vivoX60Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = vivoX60,
                Storage = "128",
                CostPrice = 500,
                SellingPrice = 599,
                StockQuantity = 90,
                ItemId = vivoX60.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Blue",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            vivoX60.Variants.Add(vivoX60Variant1);
            vivoGroup.Items.Add(vivoX60);
            groups.Add(vivoGroup);

            // ---------------------------------
            // OnePlus Series Group
            // ---------------------------------
            Guid onePlusGroupId = Guid.NewGuid();
            var onePlusGroup = new ItemGroup
            {
                ItemGroupName = "OnePlus Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var onePlus9 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "OnePlus 9",
                Description = "OnePlus 9 delivers flagship performance with a smooth display.",
                Picture = "https://example.com/oneplus9.jpg",
                ReleaseDate = new DateTime(2021, 3, 23),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = onePlusGroupId,
                Variants = new List<Variant>()
            };

            var onePlus9Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = onePlus9,
                Storage = "128",
                CostPrice = 550,
                SellingPrice = 649,
                StockQuantity = 100,
                ItemId = onePlus9.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Black",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            onePlus9.Variants.Add(onePlus9Variant1);
            onePlusGroup.Items.Add(onePlus9);
            groups.Add(onePlusGroup);

            // ---------------------------------
            // Realme Series Group
            // ---------------------------------
            Guid realmeGroupId = Guid.NewGuid();
            var realmeGroup = new ItemGroup
            {
                ItemGroupName = "Realme Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var realmeGT = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Realme GT",
                Description = "Realme GT offers powerful performance at an affordable price.",
                Picture = "https://example.com/realmegt.jpg",
                ReleaseDate = new DateTime(2021, 3, 4),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = realmeGroupId,
                Variants = new List<Variant>()
            };

            var realmeGTVariant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = realmeGT,
                Storage = "128",
                CostPrice = 400,
                SellingPrice = 499,
                StockQuantity = 150,
                ItemId = realmeGT.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Green",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            realmeGT.Variants.Add(realmeGTVariant1);
            realmeGroup.Items.Add(realmeGT);
            groups.Add(realmeGroup);

            // ---------------------------------
            // Nokia Series Group
            // ---------------------------------
            Guid nokiaGroupId = Guid.NewGuid();
            var nokiaGroup = new ItemGroup
            {
                ItemGroupName = "Nokia Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var nokia83 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Nokia 8.3",
                Description = "Nokia 8.3 features a pure Android experience and durable design.",
                Picture = "https://example.com/nokia83.jpg",
                ReleaseDate = new DateTime(2020, 9, 15),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = nokiaGroupId,
                Variants = new List<Variant>()
            };

            var nokia83Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = nokia83,
                Storage = "64",
                CostPrice = 350,
                SellingPrice = 449,
                StockQuantity = 120,
                ItemId = nokia83.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Blue",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            nokia83.Variants.Add(nokia83Variant1);
            nokiaGroup.Items.Add(nokia83);
            groups.Add(nokiaGroup);

            // ---------------------------------
            // Vivo Y Series Group
            // ---------------------------------
            Guid vivoYGroupId = Guid.NewGuid();
            var vivoYGroup = new ItemGroup
            {
                ItemGroupName = "Vivo Y Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var vivoY20 = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Vivo Y20",
                Description = "Vivo Y20 features a large battery and a modern design.",
                Picture = "https://example.com/vivoy20.jpg",
                ReleaseDate = new DateTime(2020, 7, 20),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = vivoYGroupId,
                Variants = new List<Variant>()
            };

            var vivoY20Variant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = vivoY20,
                Storage = "64",
                CostPrice = 250,
                SellingPrice = 299,
                StockQuantity = 150,
                ItemId = vivoY20.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Red",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            var vivoY20Variant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = vivoY20,
                Storage = "128",
                CostPrice = 300,
                SellingPrice = 349,
                StockQuantity = 100,
                ItemId = vivoY20.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Blue",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            vivoY20.Variants.Add(vivoY20Variant1);
            vivoY20.Variants.Add(vivoY20Variant2);
            vivoYGroup.Items.Add(vivoY20);
            groups.Add(vivoYGroup);

            // ---------------------------------
            // Motorola Moto Series Group
            // ---------------------------------
            Guid motorolaGroupId = Guid.NewGuid();
            var motorolaGroup = new ItemGroup
            {
                ItemGroupName = "Motorola Moto Series",
                Category = "Smartphones",
                Items = new List<Item>()
            };

            var motoGPower = new Item
            {
                Id = Guid.NewGuid(),
                ItemName = "Moto G Power",
                Description = "Moto G Power is known for its long-lasting battery and solid performance.",
                Picture = "https://example.com/motogpower.jpg",
                ReleaseDate = new DateTime(2020, 8, 1),
                ManufacturerId = Guid.NewGuid(),
                ItemGroupId = motorolaGroupId,
                Variants = new List<Variant>()
            };

            var motoGPowerVariant1 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = motoGPower,
                Storage = "64",
                CostPrice = 200,
                SellingPrice = 249,
                StockQuantity = 180,
                ItemId = motoGPower.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Black",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            var motoGPowerVariant2 = new Variant
            {
                Id = Guid.NewGuid(),
                Item = motoGPower,
                Storage = "128",
                CostPrice = 220,
                SellingPrice = 279,
                StockQuantity = 150,
                ItemId = motoGPower.Id,
                Color = new Color
                {
                    Id = Guid.NewGuid(),
                    Name = "Silver",
                    UrlImage = "https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:90/plain/https://cellphones.com.vn/media/catalog/product/i/p/iphone-16-pro.png"
                }
            };

            motoGPower.Variants.Add(motoGPowerVariant1);
            motoGPower.Variants.Add(motoGPowerVariant2);
            motorolaGroup.Items.Add(motoGPower);
            groups.Add(motorolaGroup);

            return groups;
        }
    }
}
