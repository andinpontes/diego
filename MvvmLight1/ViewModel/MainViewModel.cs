using GalaSoft.MvvmLight;
using MvvmLight1.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvvmLight1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private string _title = "Fußball-Ergebnisse";
        //private string _welcomeTitle = string.Empty;
        private string _matchDayTitle = string.Empty;
        private List<SoccerMatchViewModel> _soccerMatches = new List<SoccerMatchViewModel>();

        public string MainWindowTitle
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        //public string WelcomeTitle
        //{
        //    get { return _welcomeTitle; }
        //    set { Set(ref _welcomeTitle, value); }
        //}

        public string MatchDayTitle
        {
            get { return _matchDayTitle; }
            set { Set(ref _matchDayTitle, value); }
        }

        public List<SoccerMatchViewModel> SoccerMatches
        {
            get { return _soccerMatches; }
            set { Set(ref _soccerMatches, value); }
        }

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            //_dataService.GetData(
            //    (item, error) =>
            //    {
            //        if (!HandleError(error))
            //        {
            //            return;
            //        }

            //        WelcomeTitle = item.Title;
            //    });

            _dataService.GetMatches(
                (matches, error) =>
                {
                    if (!HandleError(error))
                    {
                        return;
                    }

                    SoccerMatches = Convert(matches);
                    MatchDayTitle = DetectMatchDayTitle(matches);
                });
        }

        public override void Cleanup()
        {
            // Clean up if needed

            base.Cleanup();
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

        private List<SoccerMatchViewModel> Convert(IEnumerable<SoccerMatch> matches)
        {
            return matches.Select(m => new SoccerMatchViewModel
            {
                Team1 = m.Team1.Name,
                Team2 = m.Team2.Name,
                GoalsOfTeam1 = 1, //TODO:
                GoalsOfTeam2 = 0
            }).ToList();
        }

        private string DetectMatchDayTitle(List<SoccerMatch> matches)
        {
            return $"{matches[0].LeagueName} - {matches[0].StartDate.ToShortDateString()}";
        }
    }
}