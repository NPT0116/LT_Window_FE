using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService;
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
            HttpClient httpClient = new HttpClient();
            DIContainer.AddInstance<HttpClient>(httpClient);
            DIContainer.AddKeyedSingleton<IItemApiService, ItemApiService>();

            DIContainer.AddKeyedSingleton<IPhoneRepository, MockPhoneRepository>();
            DIContainer.AddKeyedSingleton<IItemRepository, RestItemRepository>();
            // Minh Add mock data for testing
            DIContainer.AddInstance<IItemRepository>(new MockItemRepository());
            DIContainer.AddKeyedSingleton<IPhoneRepository, MockPhoneRepository>();

        }
    }
}
