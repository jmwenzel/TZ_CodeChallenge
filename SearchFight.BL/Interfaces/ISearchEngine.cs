using SearchFight.Models;
using System.Threading.Tasks;

namespace SearchFight.BL.Interfaces
{
    public interface ISearchEngine
    {
        /// <summary>
        /// Engine Name
        /// </summary>
        string Engine { get;  }

        /// <summary>
        /// Perform search
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<SearchResult> GetSearchResult(string query);
    }
}
