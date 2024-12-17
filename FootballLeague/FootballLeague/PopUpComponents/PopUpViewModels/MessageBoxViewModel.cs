using FootballLeague.Core;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballLeague.PopUpComponents.PopUpViewModels
{
    public class MessageBoxViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        public ICommand ConfirmCommand { get; }

        private string _title;
        public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }

        private string _message;
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }

        private PackIconKind _icon;
        public PackIconKind Icon { get => _icon; set { _icon = value; OnPropertyChanged(); } }

        public MessageBoxViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ConfirmCommand = new RelayCommand(_ =>
            {
                _dialogService.CloseDialog();
            });
        }
    }
}
