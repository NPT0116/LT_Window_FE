
using System;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.ViewModel.Pages;

namespace Navigation.Views
{
    public sealed partial class MainPage : Page
    {
        public readonly MainPageViewModel _viewModel;
        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = new MainPageViewModel(new DashboardPageViewModel());
            this.DataContext = _viewModel;
        }

        private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (_viewModel.GoBackCommand.CanExecute(null))
            {
                _viewModel.GoBackCommand.Execute(null);
            }
        }

    }
}
