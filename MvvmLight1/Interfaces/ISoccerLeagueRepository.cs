using System.Collections.Generic;
using MvvmLight1.Model;

namespace MvvmLight1.Interfaces
{
    public interface ISoccerLeagueRepository
    {
        List<SoccerMatch> GetSoccerMatches();
    }
}