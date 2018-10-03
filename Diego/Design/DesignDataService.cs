using System;
using System.Windows.Media.Imaging;
using Diego.Model;

namespace Diego.Design
{
    public class DesignDataService : IDataService
    {
        public void GetLeagueName(Action<string, Exception> callback)
        {
            var result = "1. Beispiel Bundesliga 2018/2019";

            callback(result, null);
        }

        public void GetMatchDay(int number, Action<SoccerMatchDay, Exception> callback)
        {
            var result = new SoccerMatchDay
            {
                Name = "1. Spieltag",
                Matches = new[]
                {
                    new SoccerMatch
                    {
                        Id = 1,
                        UtcStartDate = new DateTime(2018, 9, 21),
                        IsMatchFinished = true,
                        Team1 = new SoccerTeam
                        {
                            Id = 16,
                            Name = "VfB Stuttgart",
                            ShortName = "Stuttgart",
                            LogoUrl =new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/e/eb/VfB_Stuttgart_1893_Logo.svg/921px-VfB_Stuttgart_1893_Logo.svg.png")
                        },
                        Team2 = new SoccerTeam
                        {
                            Id = 185,
                            Name = "Fortuna Düsseldorf",
                            ShortName = "Düsseldorf",
                            LogoUrl = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/9/94/Fortuna_D%C3%BCsseldorf.svg/150px-Fortuna_D%C3%BCsseldorf.svg.png")
                        },
                        FinalResult = new SoccerMatchResult{GoalsOfTeam1 = 0, GoalsOfTeam2 = 0},
                        HalfTimeResult = new SoccerMatchResult{GoalsOfTeam1 = 0, GoalsOfTeam2 = 0},
                    },
                    new SoccerMatch
                    {
                        Id = 2,
                        UtcStartDate = new DateTime(2018, 9, 21),
                        IsMatchFinished = false,
                        Team1 = new SoccerTeam
                        {
                            Id = 1,
                            Name = "SV Meppen",
                            ShortName = "Meppen"
                        },
                        Team2 = new SoccerTeam
                        {
                            Id = 40,
                            Name = "FC Bayern München",
                            ShortName = "Bayern",
                            LogoUrl = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/Logo_FC_Bayern_M%C3%BCnchen_%282002%E2%80%932017%29.svg/600px-Logo_FC_Bayern_M%C3%BCnchen_%282002%E2%80%932017%29.svg.png"),
                            //Logo = new BitmapImage(new Uri("pack://application:,,,/Resources/Werder-Bremen-Logo.png"))
                        },
                        FinalResult = new SoccerMatchResult{GoalsOfTeam1 = 10, GoalsOfTeam2 = 0},
                        HalfTimeResult = new SoccerMatchResult{GoalsOfTeam1 = 5, GoalsOfTeam2 = 0},
                    },
                    new SoccerMatch
                    {
                        Id = 2,
                        UtcStartDate = new DateTime(2999, 9, 21),
                        IsMatchFinished = false,
                        Team1 = new SoccerTeam
                        {
                            Id = 134,
                            Name = "SV Werder Bremen",
                            ShortName = "Bremen",
                            LogoUrl = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/SV-Werder-Bremen-Logo.svg/681px-SV-Werder-Bremen-Logo.svg.png"),
                            //Logo = new BitmapImage(new Uri("pack://application:,,,/Resources/Werder-Bremen-Logo.png"))
                        },
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