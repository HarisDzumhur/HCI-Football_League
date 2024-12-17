using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FootballLeague.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region Bindings
        public ICommand SignUpCommand { get; }

        private string _username;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }

        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }

        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        private string _surname;
        public string Surname { get => _surname; set { _surname = value; OnPropertyChanged(); } }

        private string _passwordConfirmation;
        public string PasswordConfirmation { get => _passwordConfirmation; set { _passwordConfirmation = value; OnPropertyChanged(); } }
        #endregion

        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ISettingsRepository _settingsRepository;

        public SignUpViewModel(ISettingsRepository settingsRepository, IUserRepository userRepository, INavigationService navigationService, UserService userService)
        {
            NavigationService = navigationService;
            _userRepository = userRepository;
            _userService = userService;
            _settingsRepository = settingsRepository;

            SignUpCommand = new RelayCommand(ExecuteSignUp, CanExecuteSignUp);
        }

        private void ExecuteSignUp(object parameters)
        {
            if (!_userRepository.IsUsernameTaken(Username))
            {
               var user = _userRepository.AddUser(new UserModel
                {
                    Username = Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(Password),
                    PlainPassword = Password,
                    Name = Name,
                    Surname = Surname,
                    IsBlocked = false,
                    SettingsId = 1,
                    IsAdministrator = false,
                });

                var userSettings = _settingsRepository.GetUserSettings(user.SettingsId);

                ((App)Application.Current).ChangeLanguage(userSettings.Language);
                ((App)Application.Current).ChangeTheme(userSettings.Theme);
                ((App)Application.Current).ChangeFontStyle(userSettings.Font);

                _userService.CurrentUser = user;
                _userService.CurrentSettings = userSettings;

                NavigationService.NavigateTo<HomeViewModel>();
            }
            else
            {
                var messageBoxWindow = new MessageBoxWindow();
                var viewModel = new MessageBoxViewModel(new DialogService(messageBoxWindow))
                {
                    Title = "We ran into a problem",
                    Message = "That username is already taken by another user.",
                    Icon = PackIconKind.Error
                };
                messageBoxWindow.DataContext = viewModel;
                messageBoxWindow.ShowDialog();
            }
        }

        private bool CanExecuteSignUp(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(PasswordConfirmation)
                && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) && string.Equals(Password, PasswordConfirmation);
        }
    }
}
