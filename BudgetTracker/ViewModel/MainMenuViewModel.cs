using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BudgetTracker.ViewModel
{
    public partial class MainMenuViewModel : ObservableValidator
    {
        [ObservableProperty]
        private Visibility isLoggingFormVisibility;
        [ObservableProperty]
        private Visibility isProfileCreationFormVisibility;
        [ObservableProperty]
        private Visibility createProfileButtonVisibility;
        [ObservableProperty]
        private Visibility loginFormButtonVisibility;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string username;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string password;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(1)]
        private string repeatPassword;

        public MainMenuViewModel()
        {
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

        [RelayCommand]
        public void LoginProfileForm()
        {
        }

        [RelayCommand]
        public async Task CreateProfileFormAsync()
        {
            if (RepeatPassword != Password)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                await metroWindow.ShowMessageAsync("Passwords are not equal!", null);
                return;
            }
        }
    }
}
