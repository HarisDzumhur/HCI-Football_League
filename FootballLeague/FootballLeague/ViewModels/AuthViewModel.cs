using FootballLeague.Core;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FootballLeague.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private ViewModelBase _currentLeftPanelView;
        public ViewModelBase CurrentLeftPanelView
        {
            get => _currentLeftPanelView!;
            set
            {
                _currentLeftPanelView = value;
                OnPropertyChanged();
            }
        }


        private SolidColorBrush _loginButtonColor;
        public SolidColorBrush LoginButtonColor
        {
            get { return _loginButtonColor; }
            set
            {
                _loginButtonColor = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _signupButtonColor;
        public SolidColorBrush SignUpButtonColor
        {
            get { return _signupButtonColor; }
            set
            {
                _signupButtonColor = value;
                OnPropertyChanged();
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
                    ChangeLanguageCommand.Execute(value);
                }
            }
        }

        public ICommand ShowLoginView { get; }
        public ICommand ShowSignUpView { get; }
        public ICommand ChangeLanguageCommand { get; }
        public ObservableCollection<string> Languages { get; }

        public AuthViewModel(ISettingsRepository settingsRepository, IUserRepository userRepository, INavigationService navigationService)
        {
            NavigationService = navigationService;
            Languages = settingsRepository.GetEnumValues("jezik");

            ShowLoginView = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<LoginViewModel>();
                CurrentLeftPanelView = NavigationService.DynamicView;
                LoginButtonColor = Brushes.MediumPurple;
                SignUpButtonColor = Brushes.Gray;
            });

            ShowLoginView.Execute(this);

            ShowSignUpView = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<SignUpViewModel>();
                CurrentLeftPanelView = NavigationService.DynamicView;
                LoginButtonColor = Brushes.Gray;
                SignUpButtonColor = Brushes.MediumPurple;
            });

            ChangeLanguageCommand = new RelayCommand(parameter =>
            {
                if (parameter is string languageName)
                {
                    ((App)System.Windows.Application.Current).ChangeLanguage(languageName);
                }
            });

            SelectedLanguage = Languages.FirstOrDefault();
            ChangeLanguageCommand.Execute(SelectedLanguage);
        }
    }
}
