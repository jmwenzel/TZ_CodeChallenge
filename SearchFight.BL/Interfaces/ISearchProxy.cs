using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.BL.Interfaces
{
    public interface ISearchProxy
    {
        /// <summary>
        /// Group searches from different engines
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<string>> RunSearch(List<string> query);
    }
}
