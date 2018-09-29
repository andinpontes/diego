using MvvmLight1.Interfaces;
using System;

namespace MvvmLight1.Model
{
    public class DataService : IDataService
    {
        ISoccerLeagueRepository _repository = null;

        public DataService(ISoccerLeagueRepository repository)
        {
            _repository = repository;
        }

        public async void GetMatchDay(int number, Action<SoccerMatchDay, Exception> callback)
        {
            var result = new SoccerMatchDay();
            Exception error = null;

            try
            {
                result = await _repository.GetSoccerMatchDay(number);
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback(result, error);
        }

        public void GetTable(Action<SoccerTable, Exception> callback)
        {
            //TODO:
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