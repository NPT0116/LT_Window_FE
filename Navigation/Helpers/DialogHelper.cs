using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Media.AppBroadcasting;

namespace Navigation.Helpers
{
    public static class DialogHelper
    {
        public static async Task ShowDialogAsync(string title, string message, string buttonText, XamlRoot xamlRoot)
        {
            ContentDialog dialog = new ContentDialog
            {
                Content = message,
                Title = title,
                CloseButtonText = buttonText,
                RequestedTheme = ElementTheme.Light,
                XamlRoot = xamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}
