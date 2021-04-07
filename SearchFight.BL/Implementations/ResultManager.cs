using SearchFight.BL.Interfaces;
using SearchFight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.BL.Implementations
{
    public class ResultManager : IResultManager
    {
        public void TotalWinners(List<SearchResult> totalResults, List<string> messages)
        {
            var totalWinner = totalResults.OrderByDescending(o => o.SearchCount).First();
            messages.Add($"Total winner: {totalWinner.SearchQuery}");
        }

        public void EngineWinners(List<SearchResult> totalResults, List<string> messages, List<ISearchEngine> searchEngines)
        {
            foreach (var engine in searchEngines)
            {
                var result = totalResults.Where(t => t.Engine.Equals(engine.Engine)).OrderByDescending(o => o.SearchCount).First();
                messages.Add($"{engine.Engine} winner: {result.SearchQuery}");
            }
        }

        public async Task SearchResults(List<string> queryArray, List<SearchResult> totalResults, List<string> messages, List<ISearchEngine> searchEngines)
        {
            foreach (var q in queryArray)
            {
                foreach (var engine in searchEngines)
                {
                    var result = await engine.GetSearchResult(q);
                    totalResults.Add(result);
                }
                messages.Add($"{q}: {string.Join(" ", totalResults.Where(t => t.SearchQuery.Equals(q)).Select(p => p.Engine + " " + p.SearchCount))}");
            }
        }
    }
}
