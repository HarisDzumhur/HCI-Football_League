using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FootballLeague.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(); }
        }

        private readonly IUserRepository _userRepository;
        public ICommand BlockCommand { get; }
        public ICommand PromoteCommand { get; }

        public AccountsViewModel(UserService userService, IUserRepository userRepository, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _userRepository = userRepository;

            Users = _userRepository.GetAllUsers();
            Users.Remove(userService.CurrentUser);

            foreach (UserModel us in Users)
            {
                if (us.IsBlocked)
                    us.PromoteIcon = PackIconKind.None;
                if (us.IsAdministrator)
                    us.BlockIcon = PackIconKind.None;
            }

            BlockCommand = new RelayCommand(obj =>
            {
                if (obj is UserModel user)
                {
                    if (user.BlockIcon == PackIconKind.PersonBlockOutline)
                    {
                        user.BlockIcon = PackIconKind.PersonBlock;
                        user.PromoteIcon = PackIconKind.None;
                        _userRepository.SetUserStatus(user.Id, true);
                    }
                    else if (user.BlockIcon == PackIconKind.PersonBlock)
                    {
                        user.BlockIcon = PackIconKind.PersonBlockOutline;
                        user.PromoteIcon = PackIconKind.CrownOutline;
                        _userRepository.SetUserStatus(user.Id, false);
                    }
                }
            });

            PromoteCommand = new RelayCommand(obj => {
                if (obj is UserModel user)
                {
                    if (user.PromoteIcon == PackIconKind.CrownOutline)
                    {
                        user.PromoteIcon = PackIconKind.Crown;
                        user.BlockIcon = PackIconKind.None;
                        _userRepository.SetUserPromotion(user.Id, true);
                    }
                    else if (user.PromoteIcon == PackIconKind.Crown)
                    {
                        user.PromoteIcon = PackIconKind.CrownOutline;
                        user.BlockIcon = PackIconKind.PersonBlockOutline;
                        _userRepository.SetUserPromotion(user.Id, false);
                    }
                }
            });
        }
    }
}
