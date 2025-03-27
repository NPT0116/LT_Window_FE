using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Amazon.S3.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;

namespace PhoneSelling.ViewModel.Pages.Authentication
{
    public partial class LoginPageViewModel : BasePageViewModel
    {
        public RelayCommand LoginCommand { get; set; }
        [ObservableProperty] private bool isRememberMe;
        [Required(ErrorMessage ="Tên đăng nhập không được để trống.")]
        [ObservableProperty] private string userName;
        partial void OnUserNameChanged(string newValue)
        {
            ValidateProperty(newValue, nameof(UserName));
            OnPropertyChanged(nameof(UserNameError));
        }
        public string UserNameError => GetErrors(nameof(UserName)).Cast<ValidationResult>().Select(r => r.ErrorMessage).FirstOrDefault();
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]  
        [ObservableProperty] private string password;
        partial void OnPasswordChanged(string newValue)
        {
            ValidateProperty(newValue, nameof(Password));
            OnPropertyChanged(nameof(PasswordError));
        }
        public string PasswordError => GetErrors(nameof(Password)).Cast<ValidationResult>().Select(r => r.ErrorMessage).FirstOrDefault();
        // Path to store the settings file
        private readonly string settingsFilePath;

        public LoginPageViewModel()
        {
            IsRememberMe = true;
            LoginCommand = new RelayCommand(Login);

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(appDataPath, "PhoneSelling");
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }
            settingsFilePath = Path.Combine(appFolder, "settings.txt");

            // Check if a saved settings file exists
            if (File.Exists(settingsFilePath))
            {
                try
                {
                    // Expecting three lines: Username, encrypted password, and entropy.
                    var lines = File.ReadAllLines(settingsFilePath);
                    if (lines.Length >= 3)
                    {
                        var storedUsername = lines[0];
                        var encryptedInBase64 = lines[1];
                        var entropyInBase64 = lines[2];

                        var encryptedInBytes = Convert.FromBase64String(encryptedInBase64);
                        var entropyInBytes = Convert.FromBase64String(entropyInBase64);
                        var passwordInBytes = ProtectedData.Unprotect(encryptedInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
                        var storedPassword = Encoding.UTF8.GetString(passwordInBytes);

                        this.UserName = storedUsername;
                        this.Password = storedPassword;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error reading settings: " + ex.Message);
                }
            }
        }

        public void Login()
        {
            if (CanLogin())
            {
                // Caching User login information
                if (IsRememberMe)
                {
                    try
                    {
                        var passwordInBytes = Encoding.UTF8.GetBytes(Password);
                        var entropyInBytes = new byte[20];

                        using (var rng = RandomNumberGenerator.Create())
                        {
                            rng.GetBytes(entropyInBytes);
                        }
                        var encryptedInBytes = ProtectedData.Protect(passwordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
                        var encryptedInBase64 = Convert.ToBase64String(encryptedInBytes);
                        var entropyInBase64 = Convert.ToBase64String(entropyInBytes);

                        // Save the username, encrypted password and entropy in a text file
                        File.WriteAllLines(settingsFilePath, new string[]
                        {
                            UserName,
                            encryptedInBase64,
                            entropyInBase64
                        });
                        Debug.WriteLine("Caching Login Information Successfully!");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error writing settings: " + ex.Message);
                    }
                }

                // Navigation 
                ParentPageNavigation.ViewModel = new MainPageViewModel(new DashboardPageViewModel());
            }
        }

        public bool CanLogin()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                Debug.WriteLine("Error While Login !");
                Debug.WriteLine(UserNameError);
                Debug.WriteLine(PasswordError);
                OnPropertyChanged(nameof(UserNameError));
                OnPropertyChanged(nameof(PasswordError));
            }
            // Simple authentication logic for demonstration
            return UserName == "admin"
                || UserName == "minh" || UserName == "thanh" || UserName == "Quan"
                && Password == "123" && HasErrors;
        }
    }
}
