using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Repositories;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace FootballLeague.ViewModels
{
    public class AdministratorPlayerViewModel : ViewModelBase, IHandleParameters
    {
        private int? _kitNumber;
        private string _playerName;
        private string _playerSurname;
        private int? _age;
        private int _minutesPlayed;
        private int _goals;
        private int _assists;
        private int _yellowCards;
        private int _redCards;
        private int _id;
        private string _teamName;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string TeamName
        {
            get => _teamName;
            set
            {
                _teamName = value;
                OnPropertyChanged();
            }
        }

        public int? KitNumber
        {
            get => _kitNumber;
            set
            {
                _kitNumber = value;
                OnPropertyChanged();
            }
        }

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        public string PlayerSurname
        {
            get => _playerSurname;
            set
            {
                _playerSurname = value;
                OnPropertyChanged();
            }
        }

        public int? Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public int MinutesPlayed
        {
            get => _minutesPlayed;
            set
            {
                _minutesPlayed = value;
                OnPropertyChanged();
            }
        }

        public int Goals
        {
            get => _goals;
            set
            {
                _goals = value;
                OnPropertyChanged();
            }
        }

        public int Assists
        {
            get => _assists;
            set
            {
                _assists = value;
                OnPropertyChanged();
            }
        }

        public int YellowCards
        {
            get => _yellowCards;
            set
            {
                _yellowCards = value;
                OnPropertyChanged();
            }
        }

        public int RedCards
        {
            get => _redCards;
            set
            {
                _redCards = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PlayerModel> _players;
        public ObservableCollection<PlayerModel> Players
        {
            get { return _players; }
            set { _players = value; OnPropertyChanged(); }
        }

        private TeamModel _team;
        public TeamModel Team { get => _team; set { _team = value; OnPropertyChanged(); } }
        public ICommand BackCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddPlayerCommand { get; }
        private readonly ITeamRepository _teamRepository;

        public AdministratorPlayerViewModel(ITeamRepository teamRepository, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _teamRepository = teamRepository;
            

            BackCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateTo<AdministratorTeamViewModel>();
            });

            DeleteCommand = new RelayCommand(obj =>
            {
                if (obj is PlayerModel player)
                {
                    try
                    {
                        _teamRepository.RemovePlayer(player.Id);
                        Players.Remove(player);
                    }
                    catch (MySqlException ex)
                    {
                        var messageBoxWindow = new MessageBoxWindow();
                        var viewModel = new MessageBoxViewModel(new DialogService(messageBoxWindow))
                        {
                            Title = "We ran into a problem",
                            Message = ex.Message,
                            Icon = PackIconKind.Error
                        };
                        messageBoxWindow.DataContext = viewModel;
                        messageBoxWindow.ShowDialog();
                    }
                }
            });

            AddPlayerCommand = new RelayCommand(_ =>
            {
                PlayerModel player = new PlayerModel
                {
                    PlayerName = PlayerName,
                    PlayerSurname = PlayerSurname,
                    KitNumber = (int)KitNumber,
                    Goals = Goals,
                    Assists = Assists,
                    MinutesPlayed = MinutesPlayed,
                    Age = (int)Age,
                    YellowCards = YellowCards,
                    RedCards = RedCards,
                    TeamName = TeamName
                };
                Players.Add(_teamRepository.AddPlayer(player));

                PlayerName = "";
                PlayerSurname = "";
                KitNumber = 0;
                Goals = 0;
                Assists = 0;
                MinutesPlayed = 0;
                Age = 0;
                YellowCards = 0;
                RedCards = 0;
            }, CanAdd);
        }

        private bool CanAdd(object parameters)
        {
            return !string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(PlayerSurname) && Age != null && Age > 14 && KitNumber != null && KitNumber > 0;
        }

        public void HandleParameters(object? parameters)
        {
            if (parameters is TeamModel team)
            {
                TeamName = team.Name;
                Players = _teamRepository.GetAllPlayersByTeam(TeamName);
            }
        }
    }
}
