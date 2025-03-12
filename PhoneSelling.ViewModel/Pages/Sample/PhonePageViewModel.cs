using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.PhoneRepository;
using PhoneSelling.DependencyInjection;
using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Sample
{
    public class PhonePageViewModel : BasePageViewModel
    {
        private readonly IPhoneRepository _phoneRepository;
        public ObservableCollection<Phone> Phones { get; } = new ObservableCollection<Phone>();
        public PhonePageViewModel()
        {
            _phoneRepository = DIContainer.GetKeyedSingleton<IPhoneRepository>();
            LoadPhonesAsync().Wait();
        }

        private async Task LoadPhonesAsync()
        {
            var phones = await _phoneRepository.GetAll();
            Phones.Clear();

            foreach (var item in phones)
            {
                Phones.Add(item);
            }
        }
    }
}
