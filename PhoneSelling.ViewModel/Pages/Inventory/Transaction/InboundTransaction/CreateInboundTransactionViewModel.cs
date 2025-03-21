﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Common;
using PhoneSelling.Data.Common.Validators;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using PhoneSelling.Data.Repositories.InvoiceRepository;
using PhoneSelling.Data.Repositories.VariantRepository;
using PhoneSelling.Data.Repositories.VariantRepository.ApiService.Contracts.Requests;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Inventory.Transaction.InboundTransaction
{
    public partial class CreateInboundTransactionViewModel : BasePageViewModel
    {
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IVariantRepository _variantRepository;
        [ObservableProperty]
        [property:MinCollectionCount(1, ErrorMessage = "Danh sách sản phẩm không được để trống")]
        private TrulyObservableCollection<CreateInboundTransactionDto> items = new()
        {
            new CreateInboundTransactionDto { }
        };
        [ObservableProperty] private string itemsError = string.Empty;
        public CreateInboundTransactionViewModel()
        {
            _inventoryTransactionRepository = DIContainer.GetKeyedSingleton<IInventoryTransactionRepository>();
            _variantRepository = DIContainer.GetKeyedSingleton<IVariantRepository>();
        }

        public async Task<List<Variant>> SearchVariants(string text)
        {
            var query = new VariantPaginationQuery
            {
                PageNumber = 1,
                PageSize = 5,
                SearchKey = text
            };
            var paginationResult = await _variantRepository.GetAllVariants(query);
            return paginationResult.Data;
        }

        [RelayCommand]
        public void AddVariant()
        {
            Items.Add(new CreateInboundTransactionDto { });
            ItemsError = String.Empty;
        }

        [RelayCommand]
        public async Task CreateTransaction()
        {
            bool hasErrors = false;
            ValidateProperty(Items, nameof(Items));
            OnPropertyChanged(nameof(itemsError));
            var emptyItemErrorString = GetErrors(nameof(Items))?.FirstOrDefault()?.ToString() ?? "";
            ItemsError = emptyItemErrorString;
            hasErrors = true;
            foreach(var item in Items)
            {
                var errors = item.Validate();
                if(errors) hasErrors = true;
            }

            if (hasErrors) return;
            await _inventoryTransactionRepository.CreateInboundTransaction(Items.ToList());
        }

        [RelayCommand]
        public void RemoveVariant(CreateInboundTransactionDto item)
        {
            Items.Remove(item);
        }
    }
}
