using MvvmLight1.Interfaces;
using MvvmLight1.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.Repositories
{
    public class GermanSoccerLeagueRepository : ISoccerLeagueRepository
    {
        public List<SoccerMatch> GetSoccerMatches()
        {
            var result = new List<SoccerMatch>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.openligadb.de/api/getmatchdata/bl1");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("").Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<IEnumerable<JObject>>().Result;
                    result = Convert(dataObjects).ToList();
                }
                else
                {
                    int foo = 1;
                }
            }

            return result;
        }

        private IEnumerable<SoccerMatch> Convert(IEnumerable<JObject> jsonMatchObjects)
        {
            return jsonMatchObjects.Select(NewSoccerMatchByJson);
        }
        private SoccerMatch NewSoccerMatchByJson(dynamic jsonObject)
        {
            return new SoccerMatch
            {
                Id = jsonObject.MatchID,
                LeagueName = jsonObject.LeagueName,
                StartDate = jsonObject.MatchDateTime,
                IsMatchFinished = jsonObject.MatchIsFinished,
                Team1 = NewTeamByJson(jsonObject.Team1),
                Team2 = NewTeamByJson(jsonObject.Team2)
            };
        }
        private SoccerTeam NewTeamByJson(dynamic jsonObject)
        {
            string url = jsonObject.TeamIconUrl;

            return new SoccerTeam
            {
                Id = jsonObject.TeamId,
                Name = jsonObject.TeamName,
                ShortName = jsonObject.ShortName,
                IconUrl = new Uri(url)
            };
        }
    }
}
