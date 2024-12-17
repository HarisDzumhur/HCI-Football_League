using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FootballLeague.ViewModels
{
    public class HomeViewModel : ViewModelBase, IHandleParameters
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
        public ICommand ShowFavouritesCommand { get; }
        public ICommand ShowLoginCommand {  get; }


        public HomeViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            ShowSettingsCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<SettingsViewModel>();
                CurrentRightPanelView = NavigationService.DynamicView;
            });

            ShowTeamsCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<TeamViewModel>();
                CurrentRightPanelView = NavigationService.DynamicView;
            });

            ShowGamesCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<GamesViewModel>();
                CurrentRightPanelView = NavigationService.DynamicView;
            });

            ShowTablesCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<TableViewModel>();
                CurrentRightPanelView = NavigationService.DynamicView;
            });

            ShowFavouritesCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateToDynamicView<FavouritesViewModel>();
                CurrentRightPanelView = NavigationService.DynamicView;
            });

            ShowLoginCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateTo<AuthViewModel>();
            });
            ShowTablesCommand.Execute(this);
        }

        public void HandleParameters(object? parameters)
        {
            if (parameters is TeamModel team)
            {
                NavigationService.NavigateToDynamicView<PlayerViewModel>(team);
                CurrentRightPanelView = NavigationService.DynamicView;
            }
        }
    }

}
