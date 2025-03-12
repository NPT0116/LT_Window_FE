using PhoneSelling.Data.Repositories.PhoneRepository;
using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data
{
    public class DependencyInjection : IDependencyInjection
    {
        public void ConfigureServices()
        {
            DIContainer.AddKeyedSingleton<IPhoneRepository, MockPhoneRepository>();
        }
    }
}
