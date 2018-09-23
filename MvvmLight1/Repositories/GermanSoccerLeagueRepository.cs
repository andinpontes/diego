using MvvmLight1.Interfaces;
using MvvmLight1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.Repositories
{
    public class GermanSoccerLeagueRepository : ISoccerLeagueRepository
    {
        public List<SoccerMatch> GetSoccerMatches()
        {
            var result = new List<SoccerMatch>
            {
                new SoccerMatch
                {
                    Id = 1,
                    LeagueName = "1. Fußball-Bundesliga 2018/2019",
                    StartDate = new DateTime(2018, 9, 21),
                    IsMatchFinished = true,
                    Team1 = new SoccerTeam{ Id = 1, Name = "VfB Stuttgart", ShortName = "Stuttgart" },
                    Team2 = new SoccerTeam{ Id = 2, Name = "Fortuna Düsseldorf", ShortName = "Düsseldorf" },
                }
            };

            return result;
        }
    }
}
