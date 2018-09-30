namespace Diego.Model
{
    public class SoccerMatchDay
    {
        public string LeagueName { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public SoccerMatch[] Matches { get; set; }
    }
}
