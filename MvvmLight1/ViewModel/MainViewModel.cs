using GalaSoft.MvvmLight;
using MvvmLight1.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MvvmLight1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private string _title = "Fußball-Ergebnisse";
        private string _matchDayTitle = string.Empty;
        private List<SoccerMatchViewModel> _soccerMatches = new List<SoccerMatchViewModel>();

        public string MainWindowTitle
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

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
                StartDate = FormatStartDate(m.StartDate),
                Team1 = m.Team1.Name,
                Team2 = m.Team2.Name,
                MatchResult = FormatMatchResult(m),
            }).ToList();
        }

        private string DetectMatchDayTitle(List<SoccerMatch> matches)
        {
            return $"{matches[0].LeagueName} - {matches[0].StartDate.ToShortDateString()}";
        }
        private string FormatStartDate(DateTime dateTime)
        {
            string[] dayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
            string dayOfWeek = dayNames[(int)dateTime.DayOfWeek];
            string formattedDateTime = dateTime.ToString("dd.MM.yy HH:mm");

            return $"{dayOfWeek} {formattedDateTime}";
        }
        private string FormatMatchResult(SoccerMatch match)
        {
            StringBuilder sb = new StringBuilder();

            if (match.FinalResult != null)
            {
                sb.AppendFormat("{0} : {1}   ", match.FinalResult.GoalsOfTeam1, match.FinalResult.GoalsOfTeam2);
            }

            if (match.HalfTimeResult != null)
            {
                sb.AppendFormat("({0} : {1})", match.HalfTimeResult.GoalsOfTeam1, match.HalfTimeResult.GoalsOfTeam2);
            }

            return sb.ToString();
        }
    }
}