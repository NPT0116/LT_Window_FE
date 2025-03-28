using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Navigation.Views;
using PhoneSelling.ViewModel.Pages;
using PhoneSelling.Data.Repositories.CustomerRepository;
using PhoneSelling.DependencyInjection;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {

        public static Dictionary<Guid, string> CustomerDictionary { get; set; } = new Dictionary<Guid, string>();
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "vi-VN";
            this.InitializeComponent();
            var dataConfigure = new PhoneSelling.Data.DependencyInjection();
            dataConfigure.ConfigureServices();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            MainWindow.Content = new RootPage();
            MainWindow.Activate();
            LoadCustomerDictionaryAsync();
        }
        private async void LoadCustomerDictionaryAsync()
        {
            try
            {
                var customerRepository = DIContainer.GetKeyedSingleton<ICustomerRepository>();
                var customers = await customerRepository.GetAllCustomersAsync();

                // Filter out any customers with Guid.Empty, group by their ID, and build the dictionary.
                CustomerDictionary = customers.ToDictionary(c => c.CustomerID, c => c.Name);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading customer dictionary: {ex.Message}");
            }
        }

        public static Window MainWindow { get; private set; }
    }
}
