﻿using Diego.Interfaces;
using System;

namespace Diego.Model
{
    public class DataService : IDataService
    {
        ISoccerLeagueRepository _repository = null;

        public DataService(ISoccerLeagueRepository repository)
        {
            _repository = repository;
        }

        public async void GetLeagueName(Action<string, Exception> callback)
        {
            var result = "";
            Exception error = null;

            try
            {
                result = await _repository.GetLeagueName();
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback(result, error);
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

        public async void GetTable(Action<SoccerTable, Exception> callback)
        {
            var result = new SoccerTable();
            Exception error = null;

            try
            {
                result = await _repository.GetSoccerTable();
            }
            catch (Exception ex)
            {
                error = ex;
            }

            callback(result, error);
        }
    }
}