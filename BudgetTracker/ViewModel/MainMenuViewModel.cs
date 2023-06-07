using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetTracker.ViewModel
{
    public partial class MainMenuViewModel : ObservableValidator
    {
        public ObservableCollection<string> UserList { get; set; }

        [ObservableProperty]
        private Visibility isLoggingFormVisibility;
        [ObservableProperty]
        private Visibility isProfileCreationFormVisibility;
        [ObservableProperty]
        private Visibility createProfileButtonVisibility;
        [ObservableProperty]
        private Visibility loginFormButtonVisibility;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateProfileFormCommand))]
        [NotifyCanExecuteChangedFor(nameof(LoginProfileFormCommand))]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string username;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateProfileFormCommand))]
        [NotifyCanExecuteChangedFor(nameof(LoginProfileFormCommand))]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string password;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateProfileFormCommand))]
        [NotifyCanExecuteChangedFor(nameof(LoginProfileFormCommand))]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string repeatPassword;

        private readonly HttpClient _httpClient;
        public MainMenuViewModel()
        {
            _httpClient = new HttpClient();

            IsLoggingFormVisibility = Visibility.Visible;
            IsProfileCreationFormVisibility = Visibility.Collapsed;

            CreateProfileButtonVisibility = Visibility.Visible;
            LoginFormButtonVisibility = Visibility.Collapsed;
        }

        [RelayCommand]
        public void CreateProfile()
        {
            Username = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;

            IsLoggingFormVisibility = Visibility.Collapsed;
            IsProfileCreationFormVisibility = Visibility.Visible;

            CreateProfileButtonVisibility = Visibility.Collapsed;
            LoginFormButtonVisibility = Visibility.Visible;
        }

        [RelayCommand]
        public void LoginForm()
        {
            Username = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;

            IsLoggingFormVisibility = Visibility.Visible;
            IsProfileCreationFormVisibility = Visibility.Collapsed;

            CreateProfileButtonVisibility = Visibility.Visible;
            LoginFormButtonVisibility = Visibility.Collapsed;
        }

        [RelayCommand(CanExecute = nameof(CanLoginProfileForm))]
        public async Task LoginProfileForm()
        {
            var userDto = new
            {
                Username,
                Password
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7150/Authentication/login", userDto);
                if (response.IsSuccessStatusCode)
                {
                    var metroWindow = (Application.Current.MainWindow as MetroWindow);
                    await metroWindow.ShowMessageAsync("Login Successful", null);
                }
                else
                {
                    var metroWindow = (Application.Current.MainWindow as MetroWindow);
                    await metroWindow.ShowMessageAsync("Login Failed", "Invalid Credentials");
                }
            }
            catch (Exception ex)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                await metroWindow.ShowMessageAsync("Login Failed", ex.Message);
            }
        }
        private bool CanLoginProfileForm() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        [RelayCommand(CanExecute = nameof(CanCreateProfileForm))]
        public async Task CreateProfileForm()
        {
            if (RepeatPassword != Password)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                await metroWindow.ShowMessageAsync("Passwords are not equal!", null);
                return;
            }

            var userDto = new
            {
                Username,
                Password
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7150/Authentication/register", userDto);
                response.EnsureSuccessStatusCode();

                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                await metroWindow.ShowMessageAsync("Registration Successful", null);
            }
            catch (Exception ex)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                await metroWindow.ShowMessageAsync("Registration Failed", ex.Message);
            }
        }
        private bool CanCreateProfileForm() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(RepeatPassword);
    }
}
