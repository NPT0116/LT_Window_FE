using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PhoneSelling.Data.Repositories.PhoneRepository;
using PhoneSelling.ViewModel.Pages.Sample;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Diagnostics;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhonePage : Page
    {
        public ItemViewModel ViewModel { get; }
        public PhonePage()
        {
            this.InitializeComponent();
            ViewModel = new ItemViewModel();
            if (ViewModel == null)
            {
                Debug.WriteLine("❌ ERROR: ItemViewModel is NULL!");
            }
            else
            {
                Debug.WriteLine("✅ ItemViewModel initialized correctly.");
            }
            this.DataContext = ViewModel;
        }
        public void OnColorAdded(string colorName, string colorUrl)
        {
            Debug.WriteLine("This action is invoked");
            ViewModel.AddColor((colorName, colorUrl)); // ✅ Forward event data to AddColor
        }

    }
}
