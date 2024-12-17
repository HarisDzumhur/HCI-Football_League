using FootballLeague.Core;
using FootballLeague.Models;
using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.PopUpComponents.PopUpWindows;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballLeague.ViewModels
{
    public class AdministratorTeamViewModel : ViewModelBase
    {
        private ObservableCollection<TeamModel> _teams;
        public ObservableCollection<TeamModel> Teams
        {
            get { return _teams; }
            set { _teams = value; OnPropertyChanged(); }
        }

        private byte[] _selectedImage;
        public byte[] SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _coach;
        public string Coach
        {
            get => _coach;
            set
            {
                _coach = value;
                OnPropertyChanged();
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        private string _stadium;
        public string Stadium
        {
            get => _stadium;
            set
            {
                _stadium = value;
                OnPropertyChanged();
            }
        }

        private int _trophiesCount;
        public int TrophiesCount
        {
            get => _trophiesCount;
            set
            {
                _trophiesCount = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowPlayersCommand {  get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand RemoveImageCommand { get; }
        public ICommand AddTeamCommand { get; }
        public ICommand BackCommand { get; }

        private readonly ITeamRepository _teamRepository;

        public AdministratorTeamViewModel(ITeamRepository teamRepository, INavigationService navigationService) { 
            NavigationService = navigationService;
            _teamRepository = teamRepository;

            Teams = _teamRepository.GetAllTeams();

            DeleteCommand = new RelayCommand(obj =>
            {
                if (obj is TeamModel team)
                {
                    try
                    {
                        _teamRepository.RemoveTeam(team.Name);
                        Teams.Remove(team);
                    }
                    catch (MySqlException ex)
                    {
                        var messageBoxWindow = new MessageBoxWindow();
                        var viewModel = new MessageBoxViewModel(new DialogService(messageBoxWindow))
                        {
                            Title = "We ran into a problem",
                            Message = ex.Message,
                            Icon = PackIconKind.Error
                        };
                        messageBoxWindow.DataContext = viewModel;
                        messageBoxWindow.ShowDialog();
                    }
                }
            });

            ShowPlayersCommand = new RelayCommand(obj => {
                if (obj is TeamModel team)
                {
                    NavigationService.NavigateTo<AdministratorPlayerViewModel>(obj);
                }
            });

            AddImageCommand = new RelayCommand(_ =>
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png",
                    Title = "Select the club logo"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    SelectedImage = File.ReadAllBytes(openFileDialog.FileName);
                }
            });

            AddTeamCommand = new RelayCommand(_ =>
            {
                TeamModel team = new TeamModel
                {
                    Name = Name,
                    City = City,
                    Stadium = Stadium,
                    Coach = Coach,
                    TrophiesCount = TrophiesCount,
                    Logo = SelectedImage,
                    EstablishmentDate = SelectedDate
                };
                Teams.Add(team);
                _teamRepository.AddTeam(team);

                Name = "";
                City = "";
                Stadium = "";
                Coach = "";
                SelectedImage = null;
                SelectedDate = DateTime.Now;
                TrophiesCount = 0;
            }, CanAdd);

            RemoveImageCommand = new RelayCommand(_ =>
            {
                SelectedImage = null;
            });

            BackCommand = new RelayCommand(_ =>
            {
                NavigationService.NavigateTo<AdministratorHomeViewModel>();
            });
        }

        private bool CanAdd(object parameters)
        {
            return SelectedImage != null && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(Stadium) &&
                !string.IsNullOrWhiteSpace(Coach) && TrophiesCount >= 0 && SelectedDate <= DateTime.Now;
        }
    }
}
