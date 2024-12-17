using FootballLeague.Models;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.ViewModels
{
    public class FavouritesViewModel : ViewModelBase
    {
        private readonly IFavoritesRepository _favouritesRepository;
        private readonly UserService _userService;

        private ObservableCollection<FavouritesModel> _favourites;
        public ObservableCollection<FavouritesModel> Favourites { get => _favourites; set { _favourites = value; OnPropertyChanged(); } }
        public FavouritesViewModel(IFavoritesRepository favoritesRepository, INavigationService navigationService, UserService userService) 
        {
            NavigationService = navigationService;
            _favouritesRepository = favoritesRepository;
            _userService = userService;

            Favourites = _favouritesRepository.GetFavouritesByUser(_userService.CurrentUser.Id);
        }
    }
}
