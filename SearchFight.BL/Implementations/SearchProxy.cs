using SearchFight.BL.Interfaces;
using SearchFight.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchFight.BL.Implementations
{
    public class SearchProxy : ISearchProxy
    {
        public readonly List<ISearchEngine> _searchEngines;
        public readonly IResultManager _resultManager;

        public SearchProxy(List<ISearchEngine> searchEngines, IResultManager resultManager)
        {
            _searchEngines = searchEngines;
            _resultManager = resultManager;
        }

        public async Task<List<string>> RunSearch(List<string> query)
        {
            var messageList = new List<string>();

            var totalResults = new List<SearchResult>();

            // Get search results per query term
            await _resultManager.SearchResults(query, totalResults, messageList, _searchEngines);

            // Get winners per search engine
            _resultManager.EngineWinners(totalResults, messageList, _searchEngines);

            // Get Total Winner
            _resultManager.TotalWinners(totalResults, messageList);

            return messageList;
        }
        /*
        private static void TotalWinners(List<SearchResult> totalResults, List<string> messages)
        {
            var totalWinner = totalResults.OrderByDescending(o => o.SearchCount).First();
            messages.Add($"Total winner: {totalWinner.SearchQuery}");
        }

        private void EngineWinners(List<SearchResult> totalResults, List<string> messages)
        {
            foreach (var engine in _searchEngines)
            {
                var result = totalResults.Where(t => t.Engine.Equals(engine.Engine)).OrderByDescending(o => o.SearchCount).First();
                messages.Add($"{engine.Engine} winner: {result.SearchQuery}");
            }
        }

        private async Task SearchResults(List<string> queryArray, List<SearchResult> totalResults, List<string> messages)
        {
            foreach (var q in queryArray)
            {
                foreach (var engine in _searchEngines)
                {
                    var result = await engine.GetSearchResult(q);
                    totalResults.Add(result);
                }
                messages.Add($"{q}: {string.Join(" ", totalResults.Where(t => t.SearchQuery.Equals(q)).Select(p => p.Engine + " " + p.SearchCount))}");
            }
        }*/
    }
}
