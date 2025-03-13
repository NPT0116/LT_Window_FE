using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Models;
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

namespace PhoneSelling.ViewModel.Pages.Sample
{
    public class PhonePageViewModel : BasePageViewModel
    {
        public PaginationQueryViewModel<Phone> QueryViewModel { get; set; }
        private readonly IPhoneRepository _phoneRepository;
        public PhonePageViewModel() : base()
        {
            _phoneRepository = DIContainer.GetKeyedSingleton<IPhoneRepository>();
            QueryViewModel = new(LoadDataAsync);
            QueryViewModel.LoadDataCommand.Execute(null);
        }

        private async Task<IEnumerable<Phone>> LoadDataAsync()
        {
            var data = await _phoneRepository.GetAll();
            Debug.WriteLine(data.Count());
            return data;
        }
    }
}
