using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmLight1.Model
{
    public interface IDataService
    {
        void GetMatches(Action<List<SoccerMatch>, Exception> callback);
    }
}
