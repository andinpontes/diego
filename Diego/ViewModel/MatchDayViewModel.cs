﻿using GalaSoft.MvvmLight;
using Diego.Commands;
using Diego.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace Diego.ViewModel
{
    public class MatchDayViewModel : ViewModelBase
    {
        private const int NumberOfMatchesPerSeason = 34;

        private readonly IDataService _dataService;
        private readonly DispatcherTimer timer = new DispatcherTimer();

        private string _matchDayTitle = "Loading...";
        private int _matchDayNumber = 0;
        private List<SoccerMatchViewModel> _soccerMatches = new List<SoccerMatchViewModel>();
        private int _updateTimeInSeconds = 10;
        private bool _isLoading = false;

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
        public int UpdateTimeInSeconds
        {
            get { return _updateTimeInSeconds; }
            set { Set(ref _updateTimeInSeconds, value); }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public ICommand StepBackward { get; private set; }
        public ICommand StepForward { get; private set; }
        public ICommand Refresh { get; private set; }

        public MatchDayViewModel(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            StepBackward = new ActionCommand(OnStepBackward, OnCanStepBackward);
            StepForward = new ActionCommand(OnStepForward, OnCanStepForward);
            Refresh = new ActionCommand(OnRefresch, OnCanRefresh);

            UpdateMatchDayByCurrentNumber();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer.Tick += TimerElapsed;
            timer.Interval = TimeSpan.FromSeconds(_updateTimeInSeconds);
            timer.Start();
        }

        private void UpdateMatchDayByCurrentNumber()
        {
            if (IsLoading)
            {
                return;
            }

            IsLoading = true;

            _dataService.GetMatchDay(_matchDayNumber,
                (matchDay, error) =>
                {
                    if (!HandleError(error))
                    {
                        return;
                    }

                    SoccerMatches = Convert(matchDay.Matches);
                    MatchDayTitle = matchDay.Name;
                    MatchDayNumber = matchDay.Number;
                    IsLoading = false;
                });
        }

        private void TimerElapsed(object sender, EventArgs e)
        {
            UpdateMatchDayByCurrentNumber();
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

        private void OnRefresch(object obj)
        {
            UpdateMatchDayByCurrentNumber();
        }
        private bool OnCanRefresh(object arg)
        {
            return true;
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
                Team1Logo = m.Team1.Logo,
                Team2 = m.Team2.Name,
                Team2Logo = m.Team2.Logo,
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
