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

        public void GetMatches(Action<List<SoccerMatch>, Exception> callback)
        {
            var matches = _repository.GetSoccerMatches();
            callback(matches, null);
        }
    }
}