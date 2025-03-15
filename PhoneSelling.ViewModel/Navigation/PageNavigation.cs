using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.ViewModel.Base;

namespace PhoneSelling.ViewModel.Navigation
{
    public class PageNavigation : ObservableObject
    {
        private readonly HistoryStack<BasePageViewModel> historyStates;
        private readonly HistoryStack<Type> historyTypes;
        private readonly BackwardNavigationCompatibleMode backNavigationCompatibility;
        private BasePageViewModel viewModel;
        public BasePageViewModel ViewModel
        {
            get => viewModel;
            set
            {
                if (viewModel != null)
                {
                    if (backNavigationCompatibility == BackwardNavigationCompatibleMode.StoreStates)
                        historyStates.Push(viewModel);
                    else if (backNavigationCompatibility == BackwardNavigationCompatibleMode.StoreTypes)
                        historyTypes.Push(viewModel.GetType());
                }
                ChangeViewModel(value);
            }
        }
        public PageNavigation(BasePageViewModel viewModel, BackwardNavigationCompatibleMode mode = BackwardNavigationCompatibleMode.None, int maxHistory = 5)
        {
            backNavigationCompatibility = mode;
            if (mode == BackwardNavigationCompatibleMode.StoreStates)
                historyStates = new HistoryStack<BasePageViewModel>(maxHistory);
            else if (mode == BackwardNavigationCompatibleMode.StoreTypes)
                historyTypes = new HistoryStack<Type>(maxHistory);
            ViewModel = viewModel;
        }
        private void ChangeViewModel(BasePageViewModel value)
        {
            SetProperty(ref viewModel, value, nameof(ViewModel));
            viewModel.ParentPageNavigation = this;
        }
        public void GoBack()
        {
            if (backNavigationCompatibility == BackwardNavigationCompatibleMode.StoreStates && historyStates.TryPop(out var oldVm))
                ChangeViewModel(oldVm);
            if (backNavigationCompatibility == BackwardNavigationCompatibleMode.StoreTypes && historyTypes.TryPop(out var oldVmType))
                ChangeViewModel((BasePageViewModel)Activator.CreateInstance(oldVmType));
        }
        public bool CanGoBack()
        {
            if (backNavigationCompatibility == BackwardNavigationCompatibleMode.StoreStates)
                return historyStates.Items.Count > 0;
            else if (backNavigationCompatibility == BackwardNavigationCompatibleMode.StoreTypes)
                return historyTypes.Items.Count > 0;
            return false;
        }
    }
    public enum BackwardNavigationCompatibleMode
    {
        None, StoreTypes, StoreStates
    }
}
