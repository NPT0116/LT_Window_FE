using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;
using System.Buffers.Text;

namespace PhoneSelling.ViewModel.Helpers
{
    public class LocalStorageHelper
    {
        private static readonly string localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string filePath = Path.Combine(localFolder, "userSettings.json");

        public static void SaveUserSettings(string username, string encryptedPassword, string entropy)
        {
            // Create a dictionary to store the settings
            var settings = new Dictionary<string, string>
        {
            { "Username", username },
            { "Password", encryptedPassword },
            { "Entropy", entropy }
        };

            // Serialize the dictionary to JSON format
            string jsonContent = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON string to a file
            File.WriteAllText(filePath, jsonContent);
        }

        public static Dictionary<string, string> LoadUserSettings()
        {
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
            }
            return new Dictionary<string, string>();
        }
    }
}
