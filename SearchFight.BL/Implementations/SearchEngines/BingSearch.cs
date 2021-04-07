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
    public class BingSearch : ISearchEngine
    {
        public string Engine { get { return "Bing"; } }
        public async Task<SearchResult> GetSearchResult(string query)
        {
            var request = string.Format(BingConfig.BaseUrl, query);
            
            using (var client = new HttpClient { DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", BingConfig.ApiKey } } })
            {
                var response = await client.GetAsync(request);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Something went wrong. Please try again later.");

                var result = JsonConvert.DeserializeObject<BingResult>(await response.Content.ReadAsStringAsync());

                return new SearchResult()
                {
                    Engine = Engine,
                    SearchCount = long.Parse(result.WebPages.TotalEstimatedMatches),
                    SearchQuery = query
                };
            }
        }
    }
}
