using FootballLeague.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Models
{
    public class SettingsModel : ObservableObject
    {
        private string _theme;
        public string Theme { get => _theme; set {  _theme = value; OnPropertyChanged(); } }

        private int _id;
        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }

        private string _font;
        public string Font { get => _font; set { _font = value; OnPropertyChanged(); } }

        private string _language;
        public string Language { get => _language; set { _language = value; OnPropertyChanged(); } }
    }
}
