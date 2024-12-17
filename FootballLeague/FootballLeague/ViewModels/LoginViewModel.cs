using FootballLeague.Core;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Repositories;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FootballLeague.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Bindings
        public ICommand LoginCommand { get; }
        public ICommand AdministratorOption {  get; }
        public ICommand ClassicUserOption { get; }

        private bool? _isAdministrator;
        public bool? IsAdministrator { get => _isAdministrator; set { _isAdministrator = value; OnPropertyChanged(); } }

        public bool _isEnabled;
        public bool IsEnabled { get => _isEnabled; set { _isEnabled = value; OnPropertyChanged(); } }

        private SolidColorBrush _administratorButtonColor;
        public SolidColorBrush AdministratorButtonColor
        {
            get { return _administratorButtonColor; }
            set
            {
                _administratorButtonColor = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _classicUserButtonColor;
        public SolidColorBrush ClassicUserButtonColor
        {
            get { return _classicUserButtonColor; }
            set
            {
                _classicUserButtonColor = value;
                OnPropertyChanged();
            }
        }

        private string _username;
        public string Username { get => _username; 
            set { _username = value;
                IsEnabled = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
                OnPropertyChanged(); } }

        private string _password;
        public string Password { get => _password; 
            set { _password = value;
                IsEnabled = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
                OnPropertyChanged(); } }
        #endregion

        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;
        private readonly ISettingsRepository _settingsRepository;

        public LoginViewModel(ISettingsRepository settingsRepository, IUserRepository userRepository, INavigationService navigationService, UserService userService)
        {
            NavigationService = navigationService;
            _userRepository = userRepository;
            _userService = userService;
            _settingsRepository = settingsRepository;

            AdministratorButtonColor = Brushes.Gray;
            ClassicUserButtonColor = Brushes.Gray;
            IsEnabled = false;

            AdministratorOption = new RelayCommand(_ =>
            {
                AdministratorButtonColor = Brushes.MediumPurple;
                ClassicUserButtonColor = Brushes.Gray;
                IsAdministrator = true;
            });
            ClassicUserOption = new RelayCommand(_ =>
            {
                AdministratorButtonColor = Brushes.Gray;
                ClassicUserButtonColor = Brushes.MediumPurple;
                IsAdministrator = false;
            });

            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private void ExecuteLogin(object parameter)
        {
            if (_userRepository.ValidateUser(Username, Password))
            {
                var user = _userRepository.GetUserByUsername(Username);

                if (user.IsBlocked)
                {
                    var messageBoxWindow = new MessageBoxWindow();
                    var viewModel = new MessageBoxViewModel(new DialogService(messageBoxWindow))
                    {
                        Title = "We ran into a problem",
                        Message = "You have been blocked by one of the administrators.",
                        Icon = PackIconKind.Error
                    };
                    messageBoxWindow.DataContext = viewModel;
                    messageBoxWindow.ShowDialog();
                    return;
                }

                if ((bool)IsAdministrator && !user.IsAdministrator)
                {
                    var messageBoxWindow = new MessageBoxWindow();
                    var viewModel = new MessageBoxViewModel(new DialogService(messageBoxWindow))
                    {
                        Title = "We ran into a problem",
                        Message = "You don't have those kinds of privleges.",
                        Icon = PackIconKind.Error
                    };
                    messageBoxWindow.DataContext = viewModel;
                    messageBoxWindow.ShowDialog();
                    return;
                }

                var userSettings = _settingsRepository.GetUserSettings(user.SettingsId);

                ((App)Application.Current).ChangeLanguage(userSettings.Language);
                ((App)Application.Current).ChangeTheme(userSettings.Theme);
                ((App)Application.Current).ChangeFontStyle(userSettings.Font);

                _userService.CurrentSettings = userSettings;

                user.PlainPassword = Password;
                _userService.CurrentUser = user;

                if ((bool)IsAdministrator)
                {
                    NavigationService?.NavigateTo<AdministratorHomeViewModel>();
                }
                else
                {
                    NavigationService?.NavigateTo<HomeViewModel>();
                }
            }
            else
            {
                var messageBoxWindow = new MessageBoxWindow();
                var viewModel = new MessageBoxViewModel(new DialogService(messageBoxWindow))
                {
                    Title = "We ran into a problem",
                    Message = "Invalid username or password.",
                    Icon = PackIconKind.Error
                };
                messageBoxWindow.DataContext = viewModel;
                messageBoxWindow.ShowDialog();
            }
        }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && IsAdministrator != null;
        }
    }
}
