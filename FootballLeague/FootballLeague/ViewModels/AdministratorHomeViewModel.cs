using FootballLeague.Core;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballLeague.ViewModels
{
    public class AdministratorHomeViewModel : ViewModelBase
    {
        private ViewModelBase _currentRightPanelView;
        public ViewModelBase CurrentRightPanelView
        {
            get => _currentRightPanelView!;
            set
            {
                _currentRightPanelView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowSettingsCommand { get; }
        public ICommand ShowTeamsCommand { get; }
        public ICommand ShowTablesCommand { get; }
        public ICommand ShowGamesCommand { get; }
        public ICommand ShowAccountsCommand { get; }
        public ICommand ShowLoginCommand { get; }

        public AdministratorHomeViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            ShowSettingsCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<SettingsViewModel>();
                CurrentRightPanelView = NavigationService.DynamicView;
            });

            ShowTeamsCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateTo<AdministratorTeamViewModel>();
            });

            ShowGamesCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateTo<AdministratorGamesViewModel>();
            });

            ShowAccountsCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<AccountsViewModel>();
                CurrentRightPanelView = NavigationService.DynamicView;
            });

            ShowLoginCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateTo<AuthViewModel>();
            });

            ShowAccountsCommand.Execute(this);
        }
    }
}
