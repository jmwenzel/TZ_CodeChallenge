using Newtonsoft.Json;
using SearchFight.BL.Config;
using SearchFight.BL.Interfaces;
using SearchFight.BL.Models;
using SearchFight.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.BL.Implementations.SearchEngines
{
    public class GoogleSearch : ISearchEngine
    {
        public string Engine { get { return "Google"; } }

        public async Task<SearchResult> GetSearchResult(string query)
        {
            var request = string.Format(GoogleConfig.BaseUrl, GoogleConfig.ApiKey, GoogleConfig.ContextId, query);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Something went wrong. Please try again later.");

                var result = JsonConvert.DeserializeObject<GoogleResult>(await response.Content.ReadAsStringAsync());

                return new SearchResult()
                {
                    Engine = Engine,
                    SearchCount = long.Parse(result.SearchInformation.TotalResults),
                    SearchQuery = query
                };
            }
        }
    }
}
