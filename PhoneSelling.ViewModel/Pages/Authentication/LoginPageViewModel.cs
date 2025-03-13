using System.Diagnostics;
using System.Security.Cryptography;
using System;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Base;
using PhoneSelling.ViewModel.Pages.Sample;
using PhoneSelling.ViewModel.Helpers;


namespace PhoneSelling.ViewModel.Pages.Authentication
{
    public class LoginPageViewModel : BasePageViewModel
    {
        // Atributes
        public bool IsRememberme { get; set; } = false;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

        // Relay Command
        public RelayCommand LoginCommand { get; set; }

        // Constructor
        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            Dictionary<string, string> storageUser =  LocalStorageHelper.LoadUserSettings();
            if (storageUser.Count > 0)
            {
                Debug.WriteLine("Cache Working !");
                Username = storageUser["Username"];
                string encryptedInBase64 = storageUser["Password"];
                string entropyInBase64 = storageUser["Entropy"];
                var encryptedInBytes = Convert.FromBase64String(encryptedInBase64);
                var entropyInBytes = Convert.FromBase64String(entropyInBase64);
                var passwordInBytes =  ProtectedData.Unprotect(
                    encryptedInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser
                );
                Password = Encoding.UTF8.GetString(passwordInBytes);
            }
        }

        // Methods
        public void Login()
        {
            if (CanLogin())
            {
                if (IsRememberme)
                {
                    var usernameInBytes = Encoding.UTF8.GetBytes(Username);
                    var passwordInBytes = Encoding.UTF8.GetBytes(Password);
                    var entropyInBytes = new Byte[20];
                    using (var rng = RandomNumberGenerator.Create()) {
                        rng.GetBytes(entropyInBytes);
                    }
                    var encryptedInBytes = ProtectedData.Protect(
                        passwordInBytes,
                        entropyInBytes,
                        DataProtectionScope.CurrentUser
                    );
                    var encryptedInBase64 = Convert.ToBase64String(encryptedInBytes);
                    var entropyInBase64 = Convert.ToBase64String(entropyInBytes);
                    LocalStorageHelper.SaveUserSettings(Username, encryptedInBase64, entropyInBase64);
                }
                ParentPageNavigation.ViewModel = new Page1ViewModel(new Page1_1ViewModel());
            }
        }
        public bool CanLogin()
        {
           if (Username == "admin" && Password == "admin")
           {
                Debug.WriteLine("Login Successfully !");
                return true;
           } else {
                Debug.WriteLine("Cannot Login !");
                return false;
           }
        }
    }
}
