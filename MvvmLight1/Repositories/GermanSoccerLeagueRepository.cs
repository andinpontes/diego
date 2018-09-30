using MvvmLight1.Interfaces;
using MvvmLight1.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvvmLight1.Repositories
{
    public class GermanSoccerLeagueRepository : ISoccerLeagueRepository, IDisposable
    {
        private readonly string REQUEST_URL_FOR_CURRENT_MATCHDAY = "https://www.openligadb.de/api/getmatchdata/bl1";
        private readonly string REQUEST_URL_FOR_MATCHDAY_BY_YEAR_AND_NUMBER = "https://www.openligadb.de/api/getmatchdata/bl1/{0}/{1}";
        private readonly string REQUEST_URL_FOR_TABLE_BY_YEAR = "https://www.openligadb.de/api/getbltable/bl1/{0}";

        private HttpClient _httpClient = new HttpClient();

        public GermanSoccerLeagueRepository()
        {
            InitializeHttpClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<SoccerMatchDay> GetSoccerMatchDay(int number)
        {
            if (number < 0 || number > 34)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            var dataObjects = await ReadSoccerMatchDataAsync(number);

            return new SoccerMatchDay
            {
                LeagueName = ReadLeagueName(dataObjects),
                Name = ReadName(dataObjects),
                Number = ReadNumber(dataObjects),
                Matches = ReadMatches(dataObjects).ToArray()
            };
        }
        public async Task<SoccerTable> GetSoccerTable()
        {
            var dataObjects = await ReadSoccerTableAsync();

            return new SoccerTable
            {
                Entries = ReadSoccerTableEntries(dataObjects).ToArray()
            };
        }

        private void InitializeHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<IEnumerable<JObject>> ReadSoccerMatchDataAsync(int number)
        {
            var url = GetMatchDayUrlByNumber(number);

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to read soccer data.");
            }

            //await Task.Delay(3000);

            return await response.Content.ReadAsAsync<IEnumerable<JObject>>();
        }
        private string ReadLeagueName(IEnumerable<JObject> jsonMatchObjects)
        {
            dynamic firstMatch = jsonMatchObjects.FirstOrDefault();
            if (firstMatch == null)
            {
                return "<Not available>";
            }

            return firstMatch.LeagueName;
        }
        private string ReadName(IEnumerable<JObject> jsonMatchObjects)
        {
            dynamic firstMatch = jsonMatchObjects.FirstOrDefault();
            if (firstMatch == null)
            {
                return "<Not available>";
            }

            return firstMatch.Group.GroupName;
        }
        private int ReadNumber(IEnumerable<JObject> jsonMatchObjects)
        {
            dynamic firstMatch = jsonMatchObjects.FirstOrDefault();
            if (firstMatch == null)
            {
                return 0;
            }

            return firstMatch.Group.GroupOrderID;
        }
        private IEnumerable<SoccerMatch> ReadMatches(IEnumerable<JObject> jsonMatchObjects)
        {
            return jsonMatchObjects.Select(NewSoccerMatchByJson);
        }
        private SoccerMatch NewSoccerMatchByJson(dynamic jsonObject)
        {
            return new SoccerMatch
            {
                Id = jsonObject.MatchID,
                LeagueName = jsonObject.LeagueName,
                UtcStartDate = jsonObject.MatchDateTimeUTC,
                Team1 = NewTeamByJson(jsonObject.Team1),
                Team2 = NewTeamByJson(jsonObject.Team2),
                FinalResult = NewMatchResultByJson(jsonObject.MatchResults, "Endergebnis"),
                HalfTimeResult = NewMatchResultByJson(jsonObject.MatchResults, "Halbzeit"),
                IsMatchFinished = jsonObject.MatchIsFinished
            };
        }
        private SoccerTeam NewTeamByJson(dynamic jsonObject)
        {
            return new SoccerTeam
            {
                Id = jsonObject.TeamId,
                Name = jsonObject.TeamName,
                ShortName = jsonObject.ShortName,
                IconUrl = jsonObject.TeamIconUrl
            };
        }
        private SoccerMatchResult NewMatchResultByJson(dynamic jsonArray, string name)
        {
            var array = jsonArray as JArray;
            if (array == null || array.Count() <= 0)
            {
                return null;
            }

            var result = new SoccerMatchResult();

            foreach (var item in array)
            {
                if (item["ResultName"].ToString() == name)
                {
                    result.GoalsOfTeam1 = (int)item["PointsTeam1"];
                    result.GoalsOfTeam2 = (int)item["PointsTeam2"];
                    break;
                }
            }

            return result;
        }

        private async Task<IEnumerable<JObject>> ReadSoccerTableAsync()
        {
            var url = GetTableUrl();

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to read soccer table.");
            }

            return await response.Content.ReadAsAsync<IEnumerable<JObject>>();
        }
        private IEnumerable<SoccerTableEntry> ReadSoccerTableEntries(IEnumerable<JObject> jsonTableObjects)
        {
            return jsonTableObjects.Select(NewSoccerTableEntryByJson);
        }
        private SoccerTableEntry NewSoccerTableEntryByJson(dynamic jsonObject)
        {
            return new SoccerTableEntry
            {
                Team = new SoccerTeam
                {
                    Name = jsonObject.TeamName,
                    Id = jsonObject.TeamInfoId,
                    ShortName = jsonObject.ShortName,
                    IconUrl = jsonObject.TeamIconUrl
                },
                NumberOfMatches = jsonObject.Matches,
                NumberOfPoints = jsonObject.Points,
                NumberOfWonMatches = jsonObject.Won,
                NumberOfDrawMatches = jsonObject.Draw,
                NumberOfLostMatches = jsonObject.Lost,
                NumberOfGoals = jsonObject.Goals,
                NumberOfOpponentGoals = jsonObject.OpponentGoals
            };
        }

        private string GetMatchDayUrlByNumber(int number)
        {
            if (number <= 0)
            {
                return REQUEST_URL_FOR_CURRENT_MATCHDAY;
            }

            int year = GetCurrentSeasonYear();
            return string.Format(REQUEST_URL_FOR_MATCHDAY_BY_YEAR_AND_NUMBER, year, number);
        }
        private string GetTableUrl()
        {
            int year = GetCurrentSeasonYear();
            return string.Format(REQUEST_URL_FOR_TABLE_BY_YEAR, year);
        }
        private int GetCurrentSeasonYear()
        {
            //TODO:
            return 2018;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }
    }
}
