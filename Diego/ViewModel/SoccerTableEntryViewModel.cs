using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diego.ViewModel
{
    public class SoccerTableEntryViewModel
    {
        public int Position { get; set; }
        public string TeamName { get; set; }
        //Todo: Image...

        public int NumberOfMatches { get; set; }
        public int NumberOfPoints { get; set; }
        public int NumberOfWonMatches { get; set; }
        public int NumberOfLostMatches { get; set; }
        public int NumberOfDrawMatches { get; set; }
        public int NumberOfGoals { get; set; }
        public int NumberOfOpponentGoals { get; set; }
        public int GoalsDifference { get; set; }
    }
}
