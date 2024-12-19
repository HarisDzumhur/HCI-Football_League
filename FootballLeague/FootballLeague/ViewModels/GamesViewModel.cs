using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.ViewModels
{
    public class GamesViewModel : ViewModelBase
    {
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

        private ObservableCollection<GamesModel> _games;
        public ObservableCollection<GamesModel> Games { get => _games; set {  _games = value; OnPropertyChanged(); } }

        private readonly ILeagueRepository _leagueRepository;
        private readonly IGamesRepository _gamesRepository;
        public GamesViewModel(IGamesRepository gamesRepository, ILeagueRepository leagueRepository, INavigationService navigationService) 
        {
            NavigationService = navigationService;
            _leagueRepository = leagueRepository;
            _gamesRepository = gamesRepository;

            Leagues = _leagueRepository.GetAllLeagues();
        }

        public void Reset()
        {
            SelectedLeague = null;
        }
    }
}
