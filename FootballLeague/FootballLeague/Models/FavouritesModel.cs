using FootballLeague.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class FavouritesModel : ObservableObject
    {
        private string _homeTeam;
        private byte[] _homeTeamLogo;
        private ObservableCollection<ResultModel> _resultModels;
        public string HomeTeam
        {
            get { return _homeTeam; }
            set
            {
                _homeTeam = value;
                OnPropertyChanged();
            }
        }

        public byte[] HomeTeamLogo
        {
            get { return _homeTeamLogo; }
            set
            {
                _homeTeamLogo = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ResultModel> ResultModels
        {
            get { return _resultModels; }
            set
            {
                _resultModels = value;
                OnPropertyChanged();
            }
        }
    }
}
