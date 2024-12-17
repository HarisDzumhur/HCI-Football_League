using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.Repositories;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FootballLeague.ViewModels
{
    public class TeamViewModel : ViewModelBase
    {
        private ObservableCollection<TeamModel> _teams;
        public ObservableCollection<TeamModel> Teams
        {
            get { return _teams; }
            set { _teams = value; OnPropertyChanged(); }
        }
        public ICommand FollowCommand { get; }
        public ICommand ShowPlayersCommand { get; }

        private readonly ITeamRepository _teamRepository;
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly UserService _userService;

        public TeamViewModel(UserService userService, IFavoritesRepository favoritesRepository, ITeamRepository teamRepository, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _teamRepository = teamRepository;
            _favoritesRepository = favoritesRepository;
            _userService = userService;
            Teams = _teamRepository.GetAllTeams();

            var followedTeams = _favoritesRepository.GetFollowsByUser(_userService.CurrentUser.Id);
            foreach (var name in followedTeams)
            {
                foreach (var team in Teams)
                {
                    if (string.Equals(team.Name, name))
                        team.BellIcon = PackIconKind.BellRing;
                }
            }


            FollowCommand = new RelayCommand(obj =>
            {
                if (obj is TeamModel team)
                {
                    if (team.BellIcon == PackIconKind.BellOutline)
                    {
                        team.BellIcon = PackIconKind.BellRing;
                        _favoritesRepository.AddFollow(team.Name, _userService.CurrentUser.Id);
                    }
                    else if (team.BellIcon == PackIconKind.BellRing)
                    {
                        team.BellIcon = PackIconKind.BellOutline;
                        _favoritesRepository.RemoveFollow(team.Name, _userService.CurrentUser.Id);
                    }
                }
            });

            ShowPlayersCommand = new RelayCommand(obj => {
                if (obj is TeamModel team)
                {
                    NavigationService.NavigateTo<HomeViewModel>(team);
                }
            });
        }
    }
}
