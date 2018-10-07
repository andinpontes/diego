using GalaSoft.MvvmLight;
using Diego.Model;
using System;
using System.Windows.Input;

namespace Diego.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private string _mainWindowTitle = "Fußball-Ergebnisse";
        private string _leagueTitle = string.Empty;

        public string MainWindowTitle
        {
            get { return _mainWindowTitle; }
            set { Set(ref _mainWindowTitle, value); }
        }
        public string LeagueTitle
        {
            get { return _leagueTitle; }
            set { Set(ref _leagueTitle, value); }
        }

        public ICommand CloseApplication { get; private set; }

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            CloseApplication = new ApplicationCloseCommand();

            UpdateLeagueTitle();
        }

        private void UpdateLeagueTitle()
        {
            LeagueTitle = "Loading...";

            _dataService.GetLeagueName((leagueName, error) =>
            {
                if (!HandleError(error))
                {
                    return;
                }

                LeagueTitle = leagueName;
            });
        }

        private bool HandleError(Exception error)
        {
            if (error == null)
            {
                return true;
            }

            // Handle error here!
            // Logging etc.

            return false;
        }
    }
}
