using FootballLeague.Models;
using FootballLeague.Repositories;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service.Common;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.ViewModels
{
    public class TableViewModel : ViewModelBase
    {
        private ObservableCollection<StatisticsModel> _teams;
        public ObservableCollection<StatisticsModel> Teams
        {
            get { return _teams; }
            set { _teams = value; OnPropertyChanged(); }
        }

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

        private string _selectedLeague;
        public string SelectedLeague 
        {
            get => _selectedLeague;
            set
            {
                _selectedLeague  = value;
                OnPropertyChanged();
                Seasons = _leagueRepository.GetAllSeasonsByLeague(SelectedLeague);
                SelectedSeason = null;
            }
        }

        private string _selectedSeason;
        public string SelectedSeason
        { 
            get => _selectedSeason; 
            set {
                _selectedSeason = value;
                OnPropertyChanged();
                if (value != null)
                    Teams = _statisticsRepository.GetAllTeams(SelectedSeason, SelectedLeague);
                else
                    Teams = null;
            }
        }

        private readonly ILeagueRepository _leagueRepository;
        private readonly IStatisticsRepository _statisticsRepository;

        public TableViewModel(IStatisticsRepository statisticsRepository, ILeagueRepository leagueRepository, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _leagueRepository = leagueRepository;
            _statisticsRepository = statisticsRepository;

            Leagues = _leagueRepository.GetAllLeagues();
        }
    }
}
