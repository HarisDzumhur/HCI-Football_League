using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Repositories;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using FootballLeague.Views;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballLeague.ViewModels
{
    public class AdministratorGamesViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; }
        public ICommand AddGameCommand { get; }
        public ICommand AddLeagueCommand { get; }
        public ICommand AddSeasonCommand { get; }
        public ICommand AddMatchdayCommand { get; }
        public ICommand ViewHomePlayerStatisticsCommand { get; }
        public ICommand ViewAwayPlayerStatisticsCommand { get; }

        private ObservableCollection<string> _leagues;
        public ObservableCollection<string> Leagues
        {
            get { return _leagues; }
            set { _leagues = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _seasons;
        public ObservableCollection<string> Seasons
        {
            get { return _seasons; }
            set { _seasons = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _matchdays;
        public ObservableCollection<int> Matchdays
        {
            get { return _matchdays; }
            set { _matchdays = value; OnPropertyChanged(); }
        }

        private string _selectedLeague;
        public string SelectedLeague
        {
            get => _selectedLeague;
            set
            {
                _selectedLeague = value;
                OnPropertyChanged();
                Seasons = _leagueRepository.GetAllSeasonsByLeague(_selectedLeague);
                SelectedSeason = null;
                SelectedMatchday = null;
            }
        }

        private string _selectedSeason;
        public string SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                _selectedSeason = value;
                OnPropertyChanged();
                if (value != null)
                    Matchdays = _leagueRepository.GetAllMatchdaysBySeason(_selectedLeague, _selectedSeason);
                SelectedMatchday = null;
            }
        }

        private int? _selectedMatchday;
        public int? SelectedMatchday
        {
            get => _selectedMatchday;
            set
            {
                _selectedMatchday = value;
                OnPropertyChanged();
                if (value != null)
                    Games = _gamesRepository.GetGames(SelectedLeague, SelectedSeason, (int)SelectedMatchday);
                else
                    Games = null;
            }
        }

        private int? _homeTeamGoals;
        public int? HomeTeamGoals
        {
            get => _homeTeamGoals;
            set
            {
                _homeTeamGoals = value;
                OnPropertyChanged();
            }
        }

        private int? _awayTeamGoals;
        public int? AwayTeamGoals
        {
            get => _awayTeamGoals;
            set
            {
                _awayTeamGoals = value;
                OnPropertyChanged();
            }
        }

        private string _referee;
        public string Referee
        {
            get => _referee;
            set
            {
                _referee = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        private TeamModel _homeTeam;
        public TeamModel HomeTeam 
        {
            get => _homeTeam;
            set 
            {
                _homeTeam = value; 
                OnPropertyChanged();
            }
        }

        private TeamModel _awayTeam;
        public TeamModel AwayTeam 
        {
            get => _awayTeam; 
            set {
                _awayTeam = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TeamModel> _teams;
        public ObservableCollection<TeamModel> Teams { get => _teams; set { _teams = value; OnPropertyChanged(); } }

        private ObservableCollection<GamesModel> _games;
        public ObservableCollection<GamesModel> Games { get => _games; set { _games = value; OnPropertyChanged(); } }

        private readonly ILeagueRepository _leagueRepository;
        private readonly IGamesRepository _gamesRepository;
        private readonly ITeamRepository _teamRepository;

        public AdministratorGamesViewModel(ITeamRepository teamRepository, IGamesRepository gamesRepository, ILeagueRepository leagueRepository, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _leagueRepository = leagueRepository;
            _gamesRepository = gamesRepository;
            _teamRepository = teamRepository;

            Leagues = _leagueRepository.GetAllLeagues();
            Teams = _teamRepository.GetAllTeams();

            BackCommand = new RelayCommand(_ => {
                NavigationService.NavigateTo<AdministratorHomeViewModel>();
            });

            AddLeagueCommand = new RelayCommand(_ => {
                var window = new AddLeagueWindow();
                var viewModel = new AddLeagueViewModel(new DialogService(window));

                viewModel.OnLeagueAdded = leagueName =>
                {
                    try
                    {
                        _leagueRepository.AddLeague(leagueName);
                        Leagues.Add(leagueName);
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
                };

                window.DataContext = viewModel;
                window.ShowDialog();
            });

            AddGameCommand = new RelayCommand(_ => {
                Games.Add(_gamesRepository.AddGame(new GamesModel
                {
                    League = SelectedLeague,
                    Season = SelectedSeason,
                    Matchday = (int)SelectedMatchday,
                    HomeTeam = HomeTeam.Name,
                    HomeTeamGoals = (int)HomeTeamGoals,
                    AwayTeam = AwayTeam.Name,
                    AwayTeamGoals = (int)AwayTeamGoals,
                    Referee = Referee,
                    Date = SelectedDate
                }));

                HomeTeam = null;
                AwayTeam = null;
                Referee = null;
                SelectedDate = DateTime.Now;
                HomeTeamGoals = null;
                AwayTeamGoals = null;

                Games = _gamesRepository.GetGames(SelectedLeague, SelectedSeason, (int)SelectedMatchday);
            }, CanAddGame);

            AddSeasonCommand = new RelayCommand(_ => {
                var window = new AddSeasonWindow();
                var viewModel = new AddSeasonViewModel(Leagues, new DialogService(window));

                viewModel.OnSeasonAdded = (seasonYear, selectedLeague) =>
                {
                    try
                    {
                        _leagueRepository.AddSeason(seasonYear, selectedLeague);
                        Seasons = _leagueRepository.GetAllSeasonsByLeague(SelectedLeague);
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
                };

                window.DataContext = viewModel;
                window.ShowDialog();
            });

            AddMatchdayCommand = new RelayCommand(_ => {
                var window = new AddMatchdayWindow();
                var viewModel = new AddMatchdayViewModel(_leagueRepository, Leagues, new DialogService(window));

                viewModel.OnMatchdayAdded = (matchdayNumber, seasonYear, leagueName) =>
                {
                    try
                    {
                        _leagueRepository.AddMatchday(matchdayNumber, seasonYear, leagueName);
                        Matchdays = _leagueRepository.GetAllMatchdaysBySeason(SelectedLeague, SelectedSeason);
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
                };

                window.DataContext = viewModel;
                window.ShowDialog();
            });

            ViewHomePlayerStatisticsCommand = new RelayCommand(obj =>
            {
                if (obj is GamesModel gm)
                    NavigationService.NavigateTo<AdministratorPlayerStatisticsViewModel>(new Tuple<string, int>(gm.HomeTeam, gm.Id));
            });

            ViewAwayPlayerStatisticsCommand = new RelayCommand(obj =>
            {
                if (obj is GamesModel gm)
                    NavigationService.NavigateTo<AdministratorPlayerStatisticsViewModel>(new Tuple<string, int>(gm.AwayTeam, gm.Id));
            });
        }

        private bool CanAddGame(object parameter)
        {
            return !string.IsNullOrWhiteSpace(SelectedLeague) && !string.IsNullOrWhiteSpace(SelectedSeason) && SelectedMatchday != null &&
                !string.IsNullOrWhiteSpace(Referee) && HomeTeam != null && AwayTeam != null && HomeTeamGoals != null && AwayTeam != null &&
                HomeTeamGoals >= 0 && AwayTeamGoals >= 0 && HomeTeam != AwayTeam && SelectedDate <= DateTime.Now;
        }
    }
}
