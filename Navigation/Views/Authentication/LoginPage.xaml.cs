using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using Navigation.Helpers;
using PhoneSelling.ViewModel.Pages.Authentication;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            this.DataContext = new LoginPageViewModel();
            WeakReferenceMessenger.Default.Register<Message>(this, (r, m) =>
            {
                DialogHelper.ShowDialogAsync(m.status ? "ĐÚNG":"LỖI", m.message, "Đóng", this.XamlRoot);
            });
        }
    }
}
