using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Model
{
    public class UserDataStorage
    {
        private const string FileName = "UserData.bin";

        public static async Task SaveLoggedInUserAsync(string username)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(username);
                var encryptedData = await Task.Run(() => ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser));

                await File.WriteAllBytesAsync(FileName, encryptedData);
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
        }

        public static async Task<string?> LoadLoggedInUserAsync()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    var encryptedData = await File.ReadAllBytesAsync(FileName);
                    var decryptedData = await Task.Run(() => ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser));
                    var username = Encoding.UTF8.GetString(decryptedData);

                    if (!string.IsNullOrEmpty(username))
                    {
                        return username;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return null;
        }
    }
}
