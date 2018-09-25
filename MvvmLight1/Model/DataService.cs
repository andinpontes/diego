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

        public async void GetMatchDay(Action<SoccerMatchDay, Exception> callback)
        {
            var result = new SoccerMatchDay();
            Exception error = null;

            try
            {
                result = await _repository.GetSoccerMatchDay();
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback(result, error);
        }
    }
}