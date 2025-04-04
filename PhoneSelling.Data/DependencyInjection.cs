﻿using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using PhoneSelling.Data.Configurations;
using PhoneSelling.Data.Repositories.ColorRepository;
using PhoneSelling.Data.Repositories.ColorRepository.ApiService;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.Data.Repositories.CustomerRepository.ApiService;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.ApiService;
using PhoneSelling.Data.Repositories.InvoiceRepository;
using PhoneSelling.Data.Repositories.InvoiceRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemGroupRepository;
using PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.ItemRepository.ApiService;
using PhoneSelling.Data.Repositories.ManufacturerRepository;
using PhoneSelling.Data.Repositories.ManufacturerRepository.ApiService;
using PhoneSelling.Data.Repositories.PhoneRepository;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService;
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
            DIContainer.AddKeyedSingleton<IVariantApiService, VariantApiService>();
            DIContainer.AddKeyedSingleton<IInvoiceApiService,InvoiceApiService>();
            DIContainer.AddKeyedSingleton<IInventoryTransactionApiService, InventoryTransactionApiService>();

            DIContainer.AddKeyedSingleton<IPhoneRepository, MockPhoneRepository>();

            DIContainer.AddKeyedSingleton<IItemRepository, RestItemRepository>();
            DIContainer.AddKeyedSingleton<ICustomerRepository, CustomerRepository>();
            DIContainer.AddKeyedSingleton<IInvoiceRepository, InvoiceRepository>();
            DIContainer.AddKeyedSingleton<IVariantRepository, VariantRepository>();
            DIContainer.AddKeyedSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();

            DIContainer.AddKeyedSingleton<IInvoiceApiService,InvoiceApiService>();
            DIContainer.AddKeyedSingleton<IColorApiService, ColorApiService>();

            DIContainer.AddKeyedSingleton<IColorRepository, ColorRepository>();

            DIContainer.AddKeyedSingleton<IItemGroupApiService, ItemGroupApiService>();
            DIContainer.AddKeyedSingleton<IItemGroupRepository, ItemGroupRepository>();

            DIContainer.AddKeyedSingleton<IManufacturerApiService, ManufacturerApiService>();
            DIContainer.AddKeyedSingleton<IManufacturerRepository, ManufacturerRepository>();

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
