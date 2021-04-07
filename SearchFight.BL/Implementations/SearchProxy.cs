using SearchFight.BL.Interfaces;
using SearchFight.Models;
using System.Collections.Generic;
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
            _resultManager.SearchEngines = _searchEngines;
        }

        public async Task<List<string>> RunSearch(List<string> query)
        {
            var messageList = new List<string>();
            var totalResults = new List<SearchResult>();

            // Get search results per query term
            await _resultManager.SearchResults(query, totalResults, messageList);
            // Get winners per search engine
            _resultManager.EngineWinners(totalResults, messageList);
            // Get Total Winner
            _resultManager.TotalWinners(totalResults, messageList);

            return messageList;
        }
    }
}
