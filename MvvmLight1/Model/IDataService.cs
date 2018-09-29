using System;

namespace MvvmLight1.Model
{
    public interface IDataService
    {
        void GetMatchDay(int number, Action<SoccerMatchDay, Exception> callback);
        void GetTable(Action<SoccerTable, Exception> callback);
    }
}
