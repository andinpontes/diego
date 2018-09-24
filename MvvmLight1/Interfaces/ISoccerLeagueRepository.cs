using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmLight1.Model;

namespace MvvmLight1.Interfaces
{
    public interface ISoccerLeagueRepository
    {
        Task<SoccerMatch[]> GetSoccerMatches();
    }
}