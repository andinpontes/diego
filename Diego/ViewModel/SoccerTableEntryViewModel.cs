using System.Windows.Media.Imaging;

namespace Diego.ViewModel
{
    public class SoccerTableEntryViewModel
    {
        public int Position { get; set; }
        public string TeamName { get; set; }
        public BitmapSource TeamLogo { get; set; }
        public int NumberOfMatches { get; set; }
        public int NumberOfPoints { get; set; }
        public int NumberOfWonMatches { get; set; }
        public int NumberOfDrawMatches { get; set; }
        public int NumberOfLostMatches { get; set; }
        public int GoalsDifference { get; set; }
        public int NumberOfGoals { get; set; }
        public int NumberOfOpponentGoals { get; set; }

        public string Matches
        {
            get
            {
                return $"{NumberOfWonMatches} : {NumberOfDrawMatches} : {NumberOfLostMatches}";
            }
        }
        public string Goals
        {
            get
            {
                return $"{NumberOfGoals} : {NumberOfOpponentGoals}";
            }
        }
    }
}
