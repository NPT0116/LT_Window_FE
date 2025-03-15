using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Configurations
{
    public interface IConfigService
    {
        T GetSection<T>(string sectionName) where T : new();
    }
}
