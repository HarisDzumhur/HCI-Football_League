using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballLeague.ViewModels
{
    public class PlayerViewModel : ViewModelBase, IHandleParameters
    {
        #region Bindings
        private ObservableCollection<PlayerModel> _players;
        public ObservableCollection<PlayerModel> Players 
        { 
            get { return _players; }
            set { _players = value; OnPropertyChanged(); }
        }

        private string _title;
        public string Title { get => _title; set {  _title = value; OnPropertyChanged(); } }

        private TeamModel _team;
        public TeamModel Team { get => _team; set { _team = value; OnPropertyChanged(); } }
        #endregion

        ITeamRepository _teamRepository;
        public void HandleParameters(object? parameter)
        {
            if (parameter is TeamModel team)
            {
                Team = team;
                var localizedPlayers = (string)Application.Current.Resources["PlayersLowerCase"];
                Title = $"{Team.Name} {localizedPlayers}";
                Players = _teamRepository.GetAllPlayersByTeam(Team.Name);
            }
        }

        public PlayerViewModel(ITeamRepository teamRepository, INavigationService navigationService) {
            NavigationService = navigationService;
            _teamRepository = teamRepository;
        }
    }
}
