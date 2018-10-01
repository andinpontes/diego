using System;

namespace Diego.Model
{
    public interface IDataService
    {
        void GetLeagueName(Action<string, Exception> callback);
        void GetMatchDay(int number, Action<SoccerMatchDay, Exception> callback);
        void GetTable(Action<SoccerTable, Exception> callback);
    }
}
