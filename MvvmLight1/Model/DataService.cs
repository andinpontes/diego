using System;
using System.Collections.Generic;

namespace MvvmLight1.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }

        public void GetMatches(Action<List<SoccerMatch>, Exception> callback)
        {
            var matches = new List<SoccerMatch>
            {
                new SoccerMatch
                {
                    Id = 1,
                    LeagueName = "1. Fußball-Bundesliga 2018/2019",
                    StartDate = new DateTime(2018, 9, 21),
                    IsMatchFinished = true
                }
            };

            callback(matches, null);
        }
    }
}