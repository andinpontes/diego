using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.Model
{
    public class SoccerMatch
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public string LeagueName { get; set; }

        public SoccerTeam Team1 { get; set; }
        public SoccerTeam Team2 { get; set; }

        public bool IsMatchFinished { get; set; }
    }
}
