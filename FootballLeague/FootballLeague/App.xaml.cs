using FootballLeague.PopUpComponents.PopUpViewModels;
using FootballLeague.Repositories;
using FootballLeague.Repositories.Interfaces;
using FootballLeague.Service;
using FootballLeague.Service.Common;
using FootballLeague.ViewModels;
using FootballLeague.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace FootballLeague
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider? _serviceProvider;

        public App()
        {
            IServiceCollection service = new ServiceCollection();

            service.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            service.AddSingleton<MainViewModel>();
            service.AddTransient<LoginViewModel>();
            service.AddTransient<SettingsViewModel>();
            service.AddTransient<SignUpViewModel>();
            service.AddTransient<AuthViewModel>();
            service.AddTransient<TeamViewModel>();
            service.AddSingleton<GamesViewModel>();
            service.AddTransient<FavouritesViewModel>();
            service.AddSingleton<TableViewModel>();
            service.AddTransient<PlayerViewModel>();

            service.AddTransient<AccountsViewModel>();
            service.AddTransient<AdministratorTeamViewModel>();
            service.AddTransient<AdministratorPlayerViewModel>();
            service.AddSingleton<AdministratorGamesViewModel>();
            service.AddTransient<AdministratorPlayerStatisticsViewModel>();

            service.AddTransient<HomeViewModel>();
            service.AddTransient<AdministratorHomeViewModel>();

            service.AddSingleton<UserService>();

            service.AddSingleton<IUserRepository, UserRepository>();
            service.AddSingleton<ISettingsRepository, SettingsRepository>();
            service.AddSingleton<ILeagueRepository, LeagueRepository>();
            service.AddSingleton<ITeamRepository, TeamRepository>();
            service.AddSingleton<IStatisticsRepository, StatisticsRepository>();
            service.AddSingleton<IGamesRepository, GamesRepository>();
            service.AddSingleton<IFavoritesRepository, FavouritesRepository>();


            service.AddSingleton<INavigationService, NavigationService>();
            service.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = service.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider?.GetRequiredService<MainWindow>();
            mainWindow?.Show();
            base.OnStartup(e);
        }

        public void ChangeTheme(string themeName)
        {
            var applicationResources = this.Resources.MergedDictionaries;

            var customThemesDictionaries = applicationResources
                .Where(r => r.Source.OriginalString.Contains("CustomThemes"))
                .ToList();

            foreach (var customThemeDict in customThemesDictionaries)
            {
                applicationResources.Remove(customThemeDict);
            }

            var newTheme = new ResourceDictionary
            {
                Source = new Uri($"Resources/CustomThemes/{themeName}Theme.xaml", UriKind.Relative)
            };

            applicationResources.Add(newTheme);
        }

        public void ChangeFontStyle(string fontStyleName)
        {
            var applicationResources = this.Resources.MergedDictionaries;

            var fontStyleDictionaries = applicationResources
                .Where(r => r.Source.OriginalString.Contains("FontStyle"))
                .ToList();

            foreach (var fontStyleDict in fontStyleDictionaries)
            {
                applicationResources.Remove(fontStyleDict);
            }

            fontStyleName = fontStyleName.Replace(" ", "");
            var newFont = new ResourceDictionary
            {
                Source = new Uri($"Resources/FontStyles/{fontStyleName}FontStyle.xaml", UriKind.Relative)
            };

            applicationResources.Add(newFont);
        }

        public void ChangeLanguage(string languageName)
        {
            var applicationResources = this.Resources.MergedDictionaries;

            var languageDictionaries = applicationResources
                .Where(r => r.Source.OriginalString.Contains("Languages"))
                .ToList();

            foreach (var languageDict in languageDictionaries)
            {
                applicationResources.Remove(languageDict);
            }

            var newLanguage = new ResourceDictionary
            {
                Source = new Uri($"Resources/Languages/Resources.{languageName}.xaml", UriKind.Relative)
            };

            applicationResources.Add(newLanguage);

            var cultureInfo = new CultureInfo(languageName);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

    }

}
