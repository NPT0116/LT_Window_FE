using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PhoneSelling.Data.Repositories.PhoneRepository;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.ViewModel.Pages;

namespace Navigation.Views.Inventory
{
    public sealed partial class PhonePage : Page
    {
        private readonly PhonePageViewModel _viewModel;
        public PhonePage()
        {
            this.InitializeComponent();
            _viewModel = new PhonePageViewModel();
            this.DataContext = _viewModel;
        }

        private void navigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            
        }
    }
}
