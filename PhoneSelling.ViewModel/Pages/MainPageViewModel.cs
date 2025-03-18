using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Navigation;
using PhoneSelling.ViewModel.Pages.Inventory;
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
        public RelayCommand GoToVariantsDetailPageCommand { get; }
        public RelayCommand GoToVariantCommand { get; }
        public RelayCommand GoToPhoneCommand { get; }
        public RelayCommand GoToCreateInvoiceCommand { get; }
        public RelayCommand GoToInvoiceListCommand { get; }

        public MainPageViewModel(BasePageViewModel viewModel)
        {
            ChildPageNavigation = new PageNavigation(viewModel, BackwardNavigationCompatibleMode.StoreTypes);
            ChildPageNavigation.PropertyChanged += ChildPageNavigation_PropertyChanged;

            GoBackCommand = new RelayCommand(ChildPageNavigation.GoBack, ChildPageNavigation.CanGoBack);
            GoToDashboardPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new DashboardPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(DashboardPageViewModel));
            // Inventory
            GoToVariantsDetailPageCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new VariantsDetailPageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(VariantsDetailPageViewModel));
            GoToPhoneCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new PhonePageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(PhonePageViewModel));
            GoToVariantCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new VariantListViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(VariantListViewModel));
            
            // Invoice
            GoToCreateInvoiceCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new CreateInvoicePageViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(CreateInvoicePageViewModel));
            GoToInvoiceListCommand = new RelayCommand(() => ChildPageNavigation.ViewModel = new InvoiceListViewModel(), () => ChildPageNavigation.ViewModel.GetType() != typeof(InvoiceListViewModel));

        }

        private void ChildPageNavigation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChildPageNavigation.ViewModel))
            {
                GoBackCommand.NotifyCanExecuteChanged();
                GoToDashboardPageCommand.NotifyCanExecuteChanged();
                // Inventory
                GoToVariantsDetailPageCommand.NotifyCanExecuteChanged();
                GoToPhoneCommand.NotifyCanExecuteChanged();
            }
        }

        public PageNavigation ChildPageNavigation { get; set; }
    }
}
