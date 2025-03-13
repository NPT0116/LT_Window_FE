using Microsoft.UI.Xaml.Controls;
using PhoneSelling.ViewModel.Pages.Authentication;

namespace Navigation.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            this.DataContext = new LoginPageViewModel();
        }
    }
}
