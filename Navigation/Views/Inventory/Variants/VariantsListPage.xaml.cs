using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using PhoneSelling.ViewModel.Pages.Inventory.Variants;

namespace Navigation.Views.Inventory.Variants
{
    public sealed partial class VariantsListPage : Page
    {
        public VariantsListPageViewModel ViewModel { get; }

        public VariantsListPage()
        {
            this.InitializeComponent();
            ViewModel = new VariantsListPageViewModel();
            this.DataContext = ViewModel;
        }
    }
}
