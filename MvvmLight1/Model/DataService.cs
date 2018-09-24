using MvvmLight1.Interfaces;
using System;
using System.Collections.Generic;

namespace MvvmLight1.Model
{
    public class DataService : IDataService
    {
        ISoccerLeagueRepository _repository = null;

        public DataService(ISoccerLeagueRepository repository)
        {
            _repository = repository;
        }

        public void GetMatches(Action<SoccerMatchDay, Exception> callback)
        {
            var result = new SoccerMatchDay
            {
                Matches = _repository.GetSoccerMatches()
            };

            callback(result, null);
        }
    }
}