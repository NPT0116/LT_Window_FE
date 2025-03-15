using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Navigation;

namespace PhoneSelling.ViewModel.Pages
{
    public class MainPageViewModel : BasePageViewModel
    {
        public RelayCommand GoToDashboardPageCommand { get; }
        public RelayCommand GoToViewItemsPageCommand { get; }
        public RelayCommand GoToCreateItemsPageCommand { get; }
        public RelayCommand GoToReportPageCommand { get; }
        public RelayCommand GoToPhoneCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public MainPageViewModel(BasePageViewModel viewModel)
        {
            ChildPageNavigation = new PageNavigation(viewModel, BackwardNavigationCompatibleMode.StoreTypes);
            ChildPageNavigation.PropertyChanged += ChildPageNavigation_PropertyChanged;

            GoToReportPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new ReportPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(ReportPageViewModel));
            GoToDashboardPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new DashboardPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(DashboardPageViewModel));
            GoToViewItemsPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new ViewItemsPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(ViewItemsPageViewModel));
            GoToCreateItemsPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new CreateItemsPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(CreateItemsPageViewModel));
            GoToPhoneCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new PhonePageViewModel(),() => ChildPageNavigation.ViewModel.GetType() != typeof(PhonePageViewModel));
            
            GoBackCommand = new RelayCommand(ChildPageNavigation.GoBack,ChildPageNavigation.CanGoBack);
        }
        private void ChildPageNavigation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChildPageNavigation.ViewModel))
            {
                GoToDashboardPageCommand.NotifyCanExecuteChanged();
                GoToViewItemsPageCommand.NotifyCanExecuteChanged();
                GoToCreateItemsPageCommand.NotifyCanExecuteChanged();
                GoToReportPageCommand.NotifyCanExecuteChanged();
                GoToPhoneCommand.NotifyCanExecuteChanged();
                GoBackCommand.NotifyCanExecuteChanged();
            }
        }
        public PageNavigation ChildPageNavigation { get; set; }
    }
}