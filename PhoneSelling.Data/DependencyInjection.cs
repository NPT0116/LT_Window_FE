using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using PhoneSelling.Data.Configurations;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService;
using PhoneSelling.Data.Repositories.PhoneRepository;
using PhoneSelling.Data.Services.FileUpload;
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
            DIContainer.AddKeyedSingleton<IConfigService, ConfigService>();
            DIContainer.AddKeyedSingleton<IItemApiService, ItemApiService>();
            DIContainer.AddKeyedSingleton<ICustomerApiService, CustomerApiService>();

            DIContainer.AddKeyedSingleton<IPhoneRepository, MockPhoneRepository>();

            DIContainer.AddKeyedSingleton<IItemRepository, RestItemRepository>();
            DIContainer.AddKeyedSingleton<ICustomerRepository, CustomerRepository>();

            // ✅ Register AWS Image Upload Service
            var awsSettings = new AWSSettings();
            var configuration = DIContainer.GetKeyedSingleton<IConfigService>();
            awsSettings = configuration.GetSection<AWSSettings>("AWS");

            // ✅ Register AWS S3 Client
            var amazonS3Client = new AmazonS3Client(awsSettings.AccessKey, awsSettings.SecretKey, RegionEndpoint.GetBySystemName(awsSettings.Region));
            DIContainer.AddInstance<IAmazonS3>(amazonS3Client);

            // ✅ Register S3 Storage Service
            DIContainer.AddKeyedSingleton<IUploadService, AwsS3StorageService>();
        }
    }
}
