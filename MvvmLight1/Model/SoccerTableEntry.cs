namespace Diego.Model
{
    public class SoccerTableEntry
    {
        public SoccerTeam Team { get; set; }

        public int NumberOfMatches { get; set; }
        public int NumberOfPoints { get; set; }
        public int NumberOfWonMatches { get; set; }
        public int NumberOfLostMatches { get; set; }
        public int NumberOfDrawMatches { get; set; }
        public int NumberOfGoals { get; set; }
        public int NumberOfOpponentGoals { get; set; }
    }
}
