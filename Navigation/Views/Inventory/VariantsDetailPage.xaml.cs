using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.ViewModel.Pages.Inventory;

namespace Navigation.Views.Inventory
{
    public sealed partial class VariantsDetailPage : Page
    {
        public VariantsDetailPageViewModel ViewModel { get; }

        public VariantsDetailPage()
        {
            this.InitializeComponent();
            ViewModel = new VariantsDetailPageViewModel();
            this.DataContext = ViewModel;
        }
    }
}
