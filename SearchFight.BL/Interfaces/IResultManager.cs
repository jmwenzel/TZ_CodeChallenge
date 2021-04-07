using SearchFight.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.BL.Interfaces
{
    public interface IResultManager
    {
        Task SearchResults(List<string> queryArray, List<SearchResult> totalResults, List<string> messages, List<ISearchEngine> searchEngines);
        void TotalWinners(List<SearchResult> totalResults, List<string> messages);
        void EngineWinners(List<SearchResult> totalResults, List<string> messages, List<ISearchEngine> searchEngines);
    }

}
