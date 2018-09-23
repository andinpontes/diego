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
                Team1 = NewTeamByJson(jsonObject.Team1),
                Team2 = NewTeamByJson(jsonObject.Team2),
                FinalResult = NewMatchResultByJson(jsonObject.MatchResults, "Endergebnis"),
                HalfTimeResult = NewMatchResultByJson(jsonObject.MatchResults, "Halbzeit"),
                IsMatchFinished = jsonObject.MatchIsFinished
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
    }
}


//{
//   "MatchID":51150,
//   "MatchDateTime":"2018-09-21T20:30:00",
//   "TimeZoneID":"W. Europe Standard Time",
//   "LeagueId":4276,
//   "LeagueName":"1. Fußball-Bundesliga 2018/2019",
//   "MatchDateTimeUTC":"2018-09-21T18:30:00Z",
//   "Group":{
//      "GroupName":"4. Spieltag",
//      "GroupOrderID":4,
//      "GroupID":31778
//   },
//   "Team1":{
//      "TeamId":16,
//      "TeamName":"VfB Stuttgart",
//      "ShortName":"Stuttgart",
//      "TeamIconUrl":"https://upload.wikimedia.org/wikipedia/commons/thumb/e/eb/VfB_Stuttgart_1893_Logo.svg/921px-VfB_Stuttgart_1893_Logo.svg.png",
//      "TeamGroupName":null
//   },
//   "Team2":{
//      "TeamId":185,
//      "TeamName":"Fortuna Düsseldorf",
//      "ShortName":"Düsseldorf",
//      "TeamIconUrl":"https://upload.wikimedia.org/wikipedia/commons/thumb/9/94/Fortuna_D%C3%BCsseldorf.svg/150px-Fortuna_D%C3%BCsseldorf.svg.png",
//      "TeamGroupName":null
//   },
//   "LastUpdateDateTime":"2018-09-21T22:22:18.96",
//   "MatchIsFinished":true,
//   "MatchResults":[
//      {
//         "ResultID":83220,
//         "ResultName":"Endergebnis",
//         "PointsTeam1":0,
//         "PointsTeam2":0,
//         "ResultOrderID":2,
//         "ResultTypeID":2,
//         "ResultDescription":"Ergebnis nach Ende der offiziellen Spielzeit"
//      },
//      {
//         "ResultID":83221,
//         "ResultName":"Halbzeit",
//         "PointsTeam1":0,
//         "PointsTeam2":0,
//         "ResultOrderID":1,
//         "ResultTypeID":1,
//         "ResultDescription":"Zwischenstand zur Halbzeit"
//      }
//   ],
//   "Goals":[

//   ],
//   "Location":{
//      "LocationID":37,
//      "LocationCity":"Stuttgart",
//      "LocationStadium":"Mercedes Benz Arena "
//   },
//   "NumberOfViewers":null
//}
