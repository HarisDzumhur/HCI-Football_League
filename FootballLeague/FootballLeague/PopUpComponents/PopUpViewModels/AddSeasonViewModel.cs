using FootballLeague.Core;
using FootballLeague.Repositories;
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
    public class AddSeasonViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;

        public ICommand CancelCommand { get; }
        public ICommand ConfirmCommand { get; }

        public Action<string, string> OnSeasonAdded { get; set; }

        private string _seasonYear;
        public string SeasonYear
        {
            get { return _seasonYear; }
            set { _seasonYear = value; }
        }

        private string _selectedLeague;
        public string SelectedLeague { get => _selectedLeague; set { _selectedLeague = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _leagues;
        public ObservableCollection<string> Leagues
        {
            get { return _leagues; }
            set { _leagues = value; OnPropertyChanged(); }
        }
        public AddSeasonViewModel(ObservableCollection<string> leagues, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Leagues = leagues;

            CancelCommand = new RelayCommand(_ =>
            {
                _dialogService.CloseDialog();
            });

            ConfirmCommand = new RelayCommand(_ =>
            {
                OnSeasonAdded?.Invoke(SeasonYear, SelectedLeague);
                _dialogService.CloseDialog();
            }, CanConfirm);
        }
        private bool CanConfirm(object parameter)
        {
            return !string.IsNullOrWhiteSpace(SeasonYear) && SelectedLeague != null;
        }
    }
}
