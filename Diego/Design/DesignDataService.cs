using System;
using Diego.Model;

namespace Diego.Design
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
                    },
                    new SoccerMatch
                    {
                        Id = 2,
                        LeagueName = "1. Beispiel Bundesliga 2018/2019",
                        UtcStartDate = new DateTime(2999, 9, 21),
                        IsMatchFinished = false,
                        Team1 = new SoccerTeam{ Id = 1, Name = "SV Werder Bremen", ShortName = "Bremen" },
                        Team2 = new SoccerTeam{ Id = 2, Name = "Hamburger SV", ShortName = "Hamburg" },
                    }
                }
            };

            callback(result, null);
        }

        public void GetTable(Action<SoccerTable, Exception> callback)
        {
            var result = new SoccerTable
            {
                Entries = new[]
                {
                    new SoccerTableEntry
                    {
                        Team = new SoccerTeam
                        {
                            Name = "FC Bayern München",
                            ShortName = "München",
                            Id = 13,
                        },
                        NumberOfMatches = 34,
                        NumberOfPoints = 84,
                        NumberOfWonMatches = 27,
                        NumberOfDrawMatches = 3,
                        NumberOfLostMatches = 4,
                        NumberOfGoals = 92,
                        NumberOfOpponentGoals = 28,
                    },
                    new SoccerTableEntry
                    {
                        Team = new SoccerTeam
                        {
                            Name = "FC Schalke 04",
                            ShortName = "Schalke",
                            Id = 12,
                        },
                        NumberOfMatches = 34,
                        NumberOfPoints = 63,
                        NumberOfWonMatches = 18,
                        NumberOfDrawMatches = 9,
                        NumberOfLostMatches = 7,
                        NumberOfGoals = 53,
                        NumberOfOpponentGoals = 37,
                    },
                }
            };

            callback(result, null);
        }
    }
}