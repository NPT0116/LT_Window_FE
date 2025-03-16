using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Navigation;
using PhoneSelling.ViewModel.Pages.Inventory;

namespace PhoneSelling.ViewModel.Pages
{
    public class MainPageViewModel : BasePageViewModel
    {
        // Inventory
        public RelayCommand GoToItemDetailPageCommand { get; }
        public RelayCommand GoToCreateItemsPageCommand { get; }
        public RelayCommand GoToInventoryAdjustmentsPageCommand {get;}
        // Go to others Main Page
        public RelayCommand GoToDashboardPageCommand { get; }
        public RelayCommand GoToReportPageCommand { get; }
        public RelayCommand GoToPhoneCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public MainPageViewModel(BasePageViewModel viewModel)
        {
            ChildPageNavigation = new PageNavigation(viewModel, BackwardNavigationCompatibleMode.StoreTypes);
            ChildPageNavigation.PropertyChanged += ChildPageNavigation_PropertyChanged;
            // Main Page
            GoToReportPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new ReportPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(ReportPageViewModel));
            GoToDashboardPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new DashboardPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(DashboardPageViewModel));
            // Inventory Management
            GoToInventoryAdjustmentsPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new InventoryAdjustmentsPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(InventoryAdjustmentsPageViewModel));        
            GoToItemDetailPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new ItemDetailPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(ItemDetailPageViewModel));
            GoToCreateItemsPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new CreateItemsPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(CreateItemsPageViewModel));
            GoToPhoneCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new PhonePageViewModel(),() => ChildPageNavigation.ViewModel.GetType() != typeof(PhonePageViewModel));
            // Goback Command
            GoBackCommand = new RelayCommand(ChildPageNavigation.GoBack,ChildPageNavigation.CanGoBack);
        }
        private void ChildPageNavigation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChildPageNavigation.ViewModel))
            {
                GoToDashboardPageCommand.NotifyCanExecuteChanged();
                GoToItemDetailPageCommand.NotifyCanExecuteChanged();
                GoToCreateItemsPageCommand.NotifyCanExecuteChanged();
                GoToReportPageCommand.NotifyCanExecuteChanged();
                GoToPhoneCommand.NotifyCanExecuteChanged();
                GoBackCommand.NotifyCanExecuteChanged();
            }
        }
        public PageNavigation ChildPageNavigation { get; set; }
    }
}