using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Base;

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
            ParentPageNavigation.ViewModel = new MainPageViewModel(new DashboardPageViewModel());
        }
    }
}
