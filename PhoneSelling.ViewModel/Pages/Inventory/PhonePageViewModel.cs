﻿using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.ItemRepository;
using PhoneSelling.Data.Repositories.PhoneRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Inventory
{
    public class PhonePageViewModel : BasePageViewModel
    {
        public ItemViewModel ItemViewModel { get; set; }
        private IItemRepository _itemRepository;
        public PhonePageViewModel()
        {
            _itemRepository = DIContainer.GetKeyedSingleton<IItemRepository>();
        }

        private async Task<IEnumerable<Item>> LoadDataAsync()
        {
            var data = await _itemRepository.GetAll();
            Debug.WriteLine(data.Count());
            return data;
        }
    }
}
