using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Navigation;
using PhoneSelling.ViewModel.Pages.Inventory;
using PhoneSelling.ViewModel.Pages.Inventory.CreateItemPages;
using PhoneSelling.ViewModel.Pages.Inventory.ItemGroups;
using PhoneSelling.ViewModel.Pages.Inventory.Transaction.InboundTransaction;
using PhoneSelling.ViewModel.Pages.Inventory.Variants;
using PhoneSelling.ViewModel.Pages.Payments.Invoices;
using PhoneSelling.ViewModel.Pages.Payments.Invoices.InvoiceList;
using PhoneSelling.ViewModel.Pages.Variants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages
{
    public class MainPageViewModel : BasePageViewModel
    {
        public RelayCommand GoBackCommand { get; }
        public RelayCommand GoToDashboardPageCommand { get; }
        // Inventory
        public RelayCommand GoToVariantsListPageCommand { get; }
        public RelayCommand GoToVariantCommand { get; }
        public RelayCommand GoToPhoneCommand { get; }
        public RelayCommand GoToItemGroupsPageCommand { get; }
        public RelayCommand GoToCreateItemPageCommand { get; }
        public RelayCommand GoToCreateInvoiceCommand { get; }
        public RelayCommand GoToInvoiceListCommand { get; }
        public RelayCommand GoToCreateInboundTransactionCommand { get; }

        public MainPageViewModel(BasePageViewModel viewModel)
        {
            ChildPageNavigation = new PageNavigation(viewModel, BackwardNavigationCompatibleMode.StoreTypes);
            ChildPageNavigation.PropertyChanged += ChildPageNavigation_PropertyChanged;

            GoBackCommand = new RelayCommand(ChildPageNavigation.GoBack, ChildPageNavigation.CanGoBack);
            GoToDashboardPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new DashboardPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(DashboardPageViewModel));
            // Inventory
            GoToCreateItemPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new CreateItemPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(CreateItemPageViewModel));
            GoToItemGroupsPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new ItemGroupsPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(ItemGroupsPageViewModel));
            GoToVariantsListPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new VariantsListPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(VariantsListPageViewModel));
            GoToVariantCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new VariantListViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(VariantListViewModel));
            GoToPhoneCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new PhonePageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(PhonePageViewModel));
            GoToVariantCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new VariantListViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(VariantListViewModel));
            GoToVariantCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new VariantListViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(VariantListViewModel));

            // Invoice
            GoToCreateInvoiceCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new CreateInvoicePageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(CreateInvoicePageViewModel));
            GoToCreateInboundTransactionCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new CreateInboundTransactionViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(CreateInboundTransactionViewModel));

        }

        private void ChildPageNavigation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChildPageNavigation.ViewModel))
            {
                GoBackCommand.NotifyCanExecuteChanged();
                GoToDashboardPageCommand.NotifyCanExecuteChanged();
                // Inventory
                GoToCreateItemPageCommand.NotifyCanExecuteChanged();
                GoToVariantsListPageCommand.NotifyCanExecuteChanged();
                GoToPhoneCommand.NotifyCanExecuteChanged();
                GoToVariantCommand.NotifyCanExecuteChanged();
                GoToCreateItemPageCommand.NotifyCanExecuteChanged();
            }
        }

        public PageNavigation ChildPageNavigation { get; set; }
    }
}
