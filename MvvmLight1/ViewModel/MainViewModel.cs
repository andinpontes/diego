using GalaSoft.MvvmLight;
using Diego.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Diego.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const int NumberOfMatchesPerSeason = 34;

        private readonly IDataService _dataService;

        private string _mainWindowTitle = "Fußball-Ergebnisse";
        private string _leagueTitle = string.Empty;
        private string _matchDayTitle = string.Empty;
        private int _matchDayNumber = 0;
        private List<SoccerMatchViewModel> _soccerMatches = new List<SoccerMatchViewModel>();

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
        public int MatchDayNumber
        {
            get { return _matchDayNumber; }
            set { Set(ref _matchDayNumber, value); }
        }

        public ICommand StepBackward { get; private set; }
        public ICommand StepForward { get; private set; }

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            UpdateMatchDayByCurrentNumber();

            StepBackward = new ActionCommand(OnStepBackward, OnCanStepBackward);
            StepForward = new ActionCommand(OnStepForward, OnCanStepForward);
        }

        private void UpdateMatchDayByCurrentNumber()
        {
            _dataService.GetMatchDay(_matchDayNumber,
                (matchDay, error) =>
                {
                    if (!HandleError(error))
                    {
                        return;
                    }

                    SoccerMatches = Convert(matchDay.Matches);
                    LeagueTitle = matchDay.LeagueName;
                    MatchDayTitle = matchDay.Name;
                    MatchDayNumber = matchDay.Number;
                });
        }

        private void OnStepBackward(object obj)
        {
            if (MatchDayNumber > 1)
            {
                MatchDayNumber--;
                UpdateMatchDayByCurrentNumber();
            }
        }
        private bool OnCanStepBackward(object arg)
        {
            return MatchDayNumber > 1;
        }

        private void OnStepForward(object obj)
        {
            if (MatchDayNumber < NumberOfMatchesPerSeason)
            {
                MatchDayNumber++;
                UpdateMatchDayByCurrentNumber();
            }
        }
        private bool OnCanStepForward(object arg)
        {
            return MatchDayNumber < NumberOfMatchesPerSeason;
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
                StartDate = FormatStartDate(m.UtcStartDate),
                Team1 = m.Team1.Name,
                Team2 = m.Team2.Name,
                MatchResult = FormatMatchResult(m),
                MatchState = DetectMatchState(m),
                IsTeam1Winner = m.FinalResult?.GoalsOfTeam1 > m.FinalResult?.GoalsOfTeam2,
                IsTeam2Winner = m.FinalResult?.GoalsOfTeam2 > m.FinalResult?.GoalsOfTeam1,
            }).ToList();
        }

        private string FormatStartDate(DateTime utcDateTime)
        {
            var dateTime = utcDateTime.ToLocalTime();
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
        private MatchState DetectMatchState(SoccerMatch match)
        {
            if (match.IsMatchFinished)
            {
                return MatchState.Finished;
            }

            if (match.UtcStartDate < DateTime.UtcNow)
            {
                return MatchState.Started;
            }

            return MatchState.Pending;
        }
    }
}