using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballLeague.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedFontStyle;
        public string SelectedFontStyle
        {
            get => _selectedFontStyle;
            set
            {
                if (_selectedFontStyle != value)
                {
                    _selectedFontStyle = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged();
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }

        private string _confirmationPassword;
        public string ConfirmationPassword
        {
            get => _confirmationPassword;
            set
            {
                _confirmationPassword = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        private int _userID;
        public int UserID
        {
            get => _userID;
            set
            {
                _userID = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Themes { get; }
        public ObservableCollection<string> FontStyles { get; }
        public ObservableCollection<string> Languages { get; }

        public ICommand ChangeThemeCommand { get; }
        public ICommand ChangeFontStyleCommand { get; }
        public ICommand ChangeLanguageCommand { get; }
        public ICommand SaveChangesCommand {  get; }

        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;
        private readonly ISettingsRepository _settingsRepository;

        public SettingsViewModel(ISettingsRepository settingsRepository, IUserRepository userRepository, INavigationService navigationService, UserService userService)
        {
            NavigationService = navigationService;
            _userRepository = userRepository;
            _userService = userService;
            _settingsRepository = settingsRepository;

            Themes = _settingsRepository.GetEnumValues("tema");
            FontStyles = _settingsRepository.GetEnumValues("font");
            Languages = _settingsRepository.GetEnumValues("jezik");

            UserID = _userService.CurrentUser.Id;
            Username = _userService.CurrentUser.Username;
            Name = _userService.CurrentUser.Name;
            Surname = _userService.CurrentUser.Surname;

            _selectedFontStyle = _userService.CurrentSettings.Font;
            _selectedLanguage = _userService.CurrentSettings.Language;
            _selectedTheme = _userService.CurrentSettings.Theme;

            ChangeThemeCommand = new RelayCommand(parameter =>
            {
                if (parameter is string themeName)
                {
                    ((App)System.Windows.Application.Current).ChangeTheme(themeName);
                }
            });

            ChangeFontStyleCommand = new RelayCommand(parameter =>
            {
                if (parameter is string fontStyleName)
                {
                    ((App)System.Windows.Application.Current).ChangeFontStyle(fontStyleName);
                }
            });

            ChangeLanguageCommand = new RelayCommand(parameter =>
            {
                if (parameter is string languageName)
                {
                    ((App)System.Windows.Application.Current).ChangeLanguage(languageName);
                }
            });

            SaveChangesCommand = new RelayCommand(ExecuteChanges, CanExecuteChanges);
        }
        
        public void ExecuteChanges(object parameter)
        {
            int settingsId = _userService.CurrentSettings.Id;

            if (!string.Equals(SelectedFontStyle, _userService.CurrentSettings.Font) || !string.Equals(SelectedLanguage, _userService.CurrentSettings.Language) ||
                !string.Equals(SelectedTheme, _userService.CurrentSettings.Theme))
            {
                settingsId = _settingsRepository.GetSettingsId(SelectedTheme, SelectedFontStyle, SelectedLanguage);
                _userService.CurrentSettings = new SettingsModel { Id = settingsId, Font = SelectedFontStyle, Language = SelectedLanguage, Theme = SelectedTheme };
            }

            _userService.CurrentUser = new UserModel { Id = _userService.CurrentUser.Id, SettingsId = settingsId, IsBlocked = false, IsAdministrator = _userService.CurrentUser.IsAdministrator,
                Name = Name, Surname = Surname, Username = Username,
                Password = !string.IsNullOrWhiteSpace(NewPassword) ? BCrypt.Net.BCrypt.HashPassword(NewPassword) : _userService.CurrentUser.Password,
                PlainPassword = !string.IsNullOrWhiteSpace(NewPassword) ? NewPassword : _userService.CurrentUser.PlainPassword
            };

            _userRepository.UpdateUser(_userService.CurrentUser);
            ChangeThemeCommand.Execute(SelectedTheme);
            ChangeFontStyleCommand.Execute(SelectedFontStyle);
            ChangeLanguageCommand.Execute(SelectedLanguage);
            OldPassword = "";
            NewPassword = "";
            ConfirmationPassword = "";
        }

        public bool CanExecuteChanges(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || string.IsNullOrWhiteSpace(Username))
                return false;

            if (!string.IsNullOrWhiteSpace(OldPassword) && (string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmationPassword)))
                return false;

            if (!string.IsNullOrWhiteSpace(NewPassword) && (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(ConfirmationPassword)))
                return false;

            if (!string.IsNullOrWhiteSpace(ConfirmationPassword) && (string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(OldPassword)))
                return false;

            if (string.Equals(Username, _userService.CurrentUser.Username) &&
                string.Equals(Name, _userService.CurrentUser.Name) &&
                string.Equals(Surname, _userService.CurrentUser.Surname) &&
                string.Equals(SelectedTheme, _userService.CurrentSettings.Theme) &&
                string.Equals(SelectedFontStyle, _userService.CurrentSettings.Font) &&
                string.Equals(SelectedLanguage, _userService.CurrentSettings.Language) &&
                string.IsNullOrWhiteSpace(ConfirmationPassword) &&
                string.IsNullOrWhiteSpace(NewPassword) &&
                string.IsNullOrWhiteSpace(OldPassword))
                return false;

            if (!string.IsNullOrWhiteSpace(NewPassword) && !string.Equals(NewPassword, ConfirmationPassword))
                return false;

            if (!string.IsNullOrWhiteSpace(OldPassword) && !string.Equals(OldPassword, _userService.CurrentUser.PlainPassword))
                return false;

            if (!string.IsNullOrWhiteSpace(OldPassword) && !string.IsNullOrWhiteSpace(NewPassword) && string.Equals(OldPassword, NewPassword))
                return false;

            return true;
        }
    }
}
