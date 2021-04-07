using SearchFight.BL.Interfaces;
using SearchFight.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchFight.BL.Implementations
{
    public class ResultManager : IResultManager
    {
        public List<ISearchEngine> SearchEngines { get; set; }

        public void TotalWinners(List<SearchResult> totalResults, List<string> messages)
        {
            var totalWinner = totalResults.OrderByDescending(o => o.SearchCount).First();
            messages.Add($"Total winner: {totalWinner.SearchQuery}");
        }

        public void EngineWinners(List<SearchResult> totalResults, List<string> messages)
        {
            foreach (var engine in SearchEngines)
            {
                var result = totalResults.Where(t => t.Engine.Equals(engine.Engine)).OrderByDescending(o => o.SearchCount).First();
                messages.Add($"{engine.Engine} winner: {result.SearchQuery}");
            }
        }

        public async Task SearchResults(List<string> queryArray, List<SearchResult> totalResults, List<string> messages)
        {
            foreach (var q in queryArray)
            {
                foreach (var engine in SearchEngines)
                {
                    var result = await engine.GetSearchResult(q);
                    totalResults.Add(result);
                }
                messages.Add($"{q}: {string.Join(" ", totalResults.Where(t => t.SearchQuery.Equals(q)).Select(p => p.Engine + " " + p.SearchCount))}");
            }
        }
    }
}
