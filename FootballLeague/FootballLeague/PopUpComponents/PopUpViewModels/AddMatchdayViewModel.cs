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

namespace FootballLeague.PopUpComponents.PopUpViewModels
{
    public class AddMatchdayViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;

        public ICommand CancelCommand { get; }
        public ICommand ConfirmCommand { get; }

        public Action<int, string, string> OnMatchdayAdded { get; set; }

        private int? _matchday;
        public int? Matchday
        {
            get { return _matchday; }
            set { _matchday = value; }
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
        public string SelectedLeague { get => _selectedLeague; 
            set { _selectedLeague = value; OnPropertyChanged(); Seasons = _leagueRepository.GetAllSeasonsByLeague(_selectedLeague); SelectedSeason = null; } }

        private string _selectedSeason;
        public string SelectedSeason { get => _selectedSeason; set { _selectedSeason = value; OnPropertyChanged(); } }

        private readonly ILeagueRepository _leagueRepository;

        public AddMatchdayViewModel(ILeagueRepository leagueRepository, ObservableCollection<string> leagues, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Leagues = leagues;
            _leagueRepository = leagueRepository;

            CancelCommand = new RelayCommand(_ =>
            {
                _dialogService.CloseDialog();
            });
            ConfirmCommand = new RelayCommand(_ =>
            {
                OnMatchdayAdded?.Invoke((int)Matchday, SelectedSeason, SelectedLeague);
                _dialogService.CloseDialog();
            }, CanConfirm);
        }

        private bool CanConfirm(object parameter)
        {
            return Matchday != null && Matchday > 0 && SelectedLeague != null && SelectedSeason != null;
        }
    }
}
