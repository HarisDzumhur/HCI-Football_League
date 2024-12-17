using FootballLeague.Core;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballLeague.PopUpComponents.PopUpViewModels
{
    public class AddLeagueViewModel
    {
        private readonly IDialogService _dialogService;

        public ICommand CancelCommand { get; }
        public ICommand ConfirmCommand { get; }
        public Action<string> OnLeagueAdded { get; set; }

        private string _leagueName;
        public string LeagueName
        {
            get { return _leagueName; }
            set { _leagueName = value; }
        }

        public AddLeagueViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            CancelCommand = new RelayCommand(_ =>
            {
                _dialogService.CloseDialog();
            });
            ConfirmCommand = new RelayCommand(_ =>
            {
                OnLeagueAdded?.Invoke(LeagueName);
                _dialogService.CloseDialog();
            }, CanConfirm);
        }
        private bool CanConfirm(object parameter)
        {
            return !string.IsNullOrWhiteSpace(LeagueName);
        }
    }
}
