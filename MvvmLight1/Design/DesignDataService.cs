using System;
using System.Collections.Generic;
using MvvmLight1.Model;

namespace MvvmLight1.Design
{
    public class DesignDataService : IDataService
    {
        public void GetMatchDay(int number, Action<SoccerMatchDay, Exception> callback)
        {
            var result = new SoccerMatchDay
            {
                LeagueName = "1. Bundesliga",
                Name = "1. Spieltag",
                Matches = new[]
                {
                    new SoccerMatch
                    {
                        Id = 1,
                        LeagueName = "1. Beispiel Bundesliga 2018/2019",
                        UtcStartDate = new DateTime(2018, 9, 21),
                        IsMatchFinished = true,
                        Team1 = new SoccerTeam{ Id = 1, Name = "VfB Stuttgart", ShortName = "Stuttgart" },
                        Team2 = new SoccerTeam{ Id = 2, Name = "Fortuna Düsseldorf", ShortName = "Düsseldorf" },
                        FinalResult = new SoccerMatchResult{GoalsOfTeam1 = 0, GoalsOfTeam2 = 0},
                        HalfTimeResult = new SoccerMatchResult{GoalsOfTeam1 = 0, GoalsOfTeam2 = 0},
                    },
                    new SoccerMatch
                    {
                        Id = 2,
                        LeagueName = "1. Beispiel Bundesliga 2018/2019",
                        UtcStartDate = new DateTime(2018, 9, 21),
                        IsMatchFinished = false,
                        Team1 = new SoccerTeam{ Id = 1, Name = "SV Meppen", ShortName = "Meppen" },
                        Team2 = new SoccerTeam{ Id = 2, Name = "FC Bayern München", ShortName = "Bayern" },
                        FinalResult = new SoccerMatchResult{GoalsOfTeam1 = 10, GoalsOfTeam2 = 0},
                        HalfTimeResult = new SoccerMatchResult{GoalsOfTeam1 = 5, GoalsOfTeam2 = 0},
                    }
                }
            };

            callback(result, null);
        }
    }
}