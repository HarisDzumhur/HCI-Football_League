using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballLeague.ViewModels
{
    public class AdministratorPlayerStatisticsViewModel : ViewModelBase, IHandleParameters
    {
        private string _teamName;
        public string TeamName { get => _teamName; set { _teamName = value; OnPropertyChanged(); } }

        private int _gameId;
        public int GameId { get => _gameId; set { _gameId = value; OnPropertyChanged(); } }

        private readonly IGamesRepository _gamesRepository;
        private readonly ITeamRepository _teamRepository;

        private ObservableCollection<PlayerModel> _players;
        public ObservableCollection<PlayerModel> Players { get => _players; set {  _players = value; OnPropertyChanged(); } }

        private PlayerModel _selectedplayer;
        public PlayerModel SelectedPlayer { get => _selectedplayer; set { _selectedplayer = value; OnPropertyChanged(); } }

        private int _minutesPlayed;
        public int MinutesPlayed { get => _minutesPlayed; set { _minutesPlayed = value; OnPropertyChanged(); } }

        private int _goals;
        public int Goals { get => _goals; set { _goals = value; OnPropertyChanged(); } }

        private int _assists;
        public int Assists { get => _assists; set { _assists = value; OnPropertyChanged(); } }


        private bool _isStarterChecked;
        public bool IsStarterChecked
        {
            get => _isStarterChecked;
            set
            {
                _isStarterChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isRedCardChecked;
        public bool IsRedCardChecked
        {
            get => _isRedCardChecked;
            set
            {
                _isRedCardChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isYellowCardChecked;
        public bool IsYellowCardChecked
        {
            get => _isYellowCardChecked;
            set
            {
                _isYellowCardChecked = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PlayerStatisticsModel> _playersStatistics;
        public ObservableCollection<PlayerStatisticsModel> PlayersStatistics { get => _playersStatistics; set { _playersStatistics = value; OnPropertyChanged(); } }

        public ICommand AddPlayerStatisticsCommand { get; }
        public ICommand BackCommand { get; }

        public AdministratorPlayerStatisticsViewModel(ITeamRepository teamRepository, IGamesRepository gamesRepository, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _gamesRepository = gamesRepository;
            _teamRepository = teamRepository;

            BackCommand = new RelayCommand(_ => {
                NavigationService.NavigateTo<AdministratorGamesViewModel>();
            });

            AddPlayerStatisticsCommand = new RelayCommand(_ => {
                PlayerStatisticsModel ps = new PlayerStatisticsModel
                {
                    GameId = GameId,
                    TeamName = TeamName,
                    PlayerId = SelectedPlayer.Id,
                    Name = SelectedPlayer.PlayerName,
                    Surname = SelectedPlayer.PlayerSurname,
                    MinutesPlayed = MinutesPlayed,
                    Goals = Goals,
                    Assists = Assists,
                    IsStarter = IsStarterChecked,
                    RedCard = IsRedCardChecked,
                    YellowCard = IsYellowCardChecked
                };
                _gamesRepository.AddPlayerStatistics(ps);
                PlayersStatistics.Add(ps);

                MinutesPlayed = 0;
                Goals = 0;
                Assists = 0;
                SelectedPlayer = null;
                IsRedCardChecked = false;
                IsStarterChecked = false;
                IsYellowCardChecked = false;
            }, CanAdd);
        }

        private bool CanAdd(object parameters)
        {
            return SelectedPlayer != null && MinutesPlayed > 0 && MinutesPlayed < 91;
        }

        public void HandleParameters(object? parameters)
        {
            if (parameters is Tuple<string, int> tu)
            {
                TeamName = tu.Item1;
                GameId = tu.Item2;
                Players = _teamRepository.GetAllPlayersByTeam(TeamName);
                PlayersStatistics = _gamesRepository.GetPlayerStatistics(TeamName, GameId);

                var playersToRemove = new List<PlayerModel>();

                foreach (var player in Players)
                {
                    foreach (var ps in PlayersStatistics)
                    {
                        if (player.Id == ps.PlayerId)
                        {
                            playersToRemove.Add(player);
                            break;
                        }
                    }
                }

                foreach (var player in playersToRemove)
                {
                    Players.Remove(player);
                }
            }
        }
    }
}
