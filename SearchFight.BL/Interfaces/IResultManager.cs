using SearchFight.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.BL.Interfaces
{
    public interface IResultManager
    {
        List<ISearchEngine> SearchEngines { get; set; }
        Task SearchResults(List<string> queryArray, List<SearchResult> totalResults, List<string> messages);
        void TotalWinners(List<SearchResult> totalResults, List<string> messages);
        void EngineWinners(List<SearchResult> totalResults, List<string> messages);
    }

}
