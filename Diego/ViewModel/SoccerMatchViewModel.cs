namespace Diego.ViewModel
{
    public class SoccerMatchViewModel
    {
        public string StartDate { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Teams
        {
            get { return $"{Team1} - {Team2}"; }
        }
        public string MatchResult { get; set; }
        public MatchState MatchState { get; set; }
        public bool IsTeam1Winner { get; set; }
        public bool IsTeam2Winner { get; set; }
    }
}
