using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Pages.Sample;

namespace PhoneSelling.ViewModel.Pages.Authentication
{
    public class LoginPageViewModel : BasePageViewModel
    {
        public RelayCommand LoginCommand { get; set; }
        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        public void Login()
        {
            ParentPageNavigation.ViewModel = new Page1ViewModel(new Page1_1ViewModel());
        }
    }
}
