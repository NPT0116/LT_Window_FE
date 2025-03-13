using PhoneSelling.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.Abstraction
{
    public abstract class BaseApiService
    {
        protected readonly string BaseApiUrl = "http://localhost:5142/api/";
        protected abstract string Prefix {  get; }
        protected string ApiUrl => BaseApiUrl + Prefix;
        protected HttpClient _httpClient;

        protected BaseApiService() 
        {
            _httpClient = DIContainer.GetKeyedSingleton<HttpClient>();
        }
    }
}
