using GalaSoft.MvvmLight;
using Diego.Model;
using System;
using System.Collections.Generic;

namespace Diego.ViewModel
{
    public class TableViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private List<SoccerTableEntryViewModel> _entries = new List<SoccerTableEntryViewModel>();

        public List<SoccerTableEntryViewModel> Entries
        {
            get { return _entries; }
            set { Set(ref _entries, value); }
        }

        public TableViewModel(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            UpdateTable();
        }

        private void UpdateTable()
        {
            _dataService.GetTable((soccerTable, error) =>
                {
                    if (!HandleError(error))
                    {
                        return;
                    }

                    Entries = Convert(soccerTable);
                });
        }

        private List<SoccerTableEntryViewModel> Convert(SoccerTable soccerTable)
        {
            var result = new List<SoccerTableEntryViewModel>();

            for (int index = 0; index < soccerTable.Entries.Length; index++)
            {
                var current = soccerTable.Entries[index];
                var item = new SoccerTableEntryViewModel
                {
                    Position = index + 1,
                    TeamName = current.Team.Name,
                    NumberOfMatches = current.NumberOfMatches,
                    NumberOfPoints = current.NumberOfPoints,
                    NumberOfWonMatches = current.NumberOfWonMatches,
                    NumberOfDrawMatches = current.NumberOfDrawMatches,
                    NumberOfLostMatches = current.NumberOfLostMatches,
                    NumberOfGoals = current.NumberOfGoals,
                    NumberOfOpponentGoals = current.NumberOfOpponentGoals,
                    GoalsDifference = current.NumberOfGoals - current.NumberOfOpponentGoals,
                };

                result.Add(item);
            }

            return result;
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
    }
}
