using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Constants
{
    public class StorageCapacity
    {
        public static readonly IReadOnlyList<int> SupportedCapacities = new List<int>
        {
            128,
            256,
            512,
            1024
        };
    }
}
