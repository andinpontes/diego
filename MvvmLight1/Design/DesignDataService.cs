using System;
using System.Collections.Generic;
using MvvmLight1.Model;

namespace MvvmLight1.Design
{
    public class DesignDataService : IDataService
    {
        public void GetMatchDay(Action<SoccerMatchDay, Exception> callback)
        {
            var result = new SoccerMatchDay
            {
                Matches = new[]
                {
                    new SoccerMatch
                    {
                        Id = 1,
                        LeagueName = "1. Beispiel Bundesliga 2018/2019",
                        StartDate = new DateTime(2018, 9, 21),
                        IsMatchFinished = true,
                        Team1 = new SoccerTeam{ Id = 1, Name = "VfB Stuttgart", ShortName = "Stuttgart" },
                        Team2 = new SoccerTeam{ Id = 2, Name = "Fortuna Düsseldorf", ShortName = "Düsseldorf" },
                    }
                }
            };

            callback(result, null);
        }
    }
}