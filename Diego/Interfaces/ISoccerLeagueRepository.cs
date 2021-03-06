﻿using System.Threading.Tasks;
using Diego.Model;

namespace Diego.Interfaces
{
    public interface ISoccerLeagueRepository
    {
        Task<string> GetLeagueName();
        Task<SoccerMatchDay> GetSoccerMatchDay(int number);
        Task<SoccerTable> GetSoccerTable();
    }
}