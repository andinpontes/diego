using System;

namespace Diego.Model
{
    public class SoccerMatch
    {
        public int Id { get; set; }
        public string LeagueName { get; set; }
        public DateTime UtcStartDate { get; set; }

        public SoccerTeam Team1 { get; set; }
        public SoccerTeam Team2 { get; set; }

        public SoccerMatchResult FinalResult { get; set; }
        public SoccerMatchResult HalfTimeResult { get; set; }

        public bool IsMatchFinished { get; set; }
    }
}
