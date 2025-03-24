using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.ViewModel.Navigation;
using PhoneSelling.ViewModel.Pages.Authentication;

namespace PhoneSelling.ViewModel.Base
{
    public class RootPageViewModel : ObservableObject
    {
        public RootPageViewModel()
        {
            ChildPageNavigation = new PageNavigation(new LoginPageViewModel());
        }
        public PageNavigation ChildPageNavigation { get; }
    }
}
