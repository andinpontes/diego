using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.ViewModel
{
    public class SoccerMatchViewModel
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }

        public int GoalsOfTeam1 { get; set; }
        public int GoalsOfTeam2 { get; set; }
    }
}
