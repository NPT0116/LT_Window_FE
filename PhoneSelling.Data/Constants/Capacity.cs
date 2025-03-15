using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Constants
{
    public class StorageCapacity
    {
        public static readonly Dictionary<int, string> SupportedCapacities = new()
        {
            { 128, "128GB" },
            { 256, "256GB" },
            { 512, "512GB" },
            { 1024, "1TB" }
        };


    }
}
