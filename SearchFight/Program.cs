using SearchFight.BL.Implementations;
using SearchFight.BL.Implementations.SearchEngines;
using SearchFight.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchFight
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*if (args.Length == 0)
            {
                Console.WriteLine("Please enter at least one search term");
                return;
            }*/

            var arg = new List<string> { "net" }; 

            var searchEngines = GetSearchEngineImplementations();

            var searchProxy = new SearchProxy(searchEngines, new ResultManager());
            
            var messages = await searchProxy.RunSearch(arg);
            messages.ForEach(m => Console.WriteLine(m));

            Console.ReadKey();
        }

        /// <summary>
        /// Get classes that implement ISearchEngine interface
        /// </summary>
        /// <returns></returns>
        private static List<ISearchEngine> GetSearchEngineImplementations()
        {
            IEnumerable<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                ?.Where(assembly => assembly.FullName.StartsWith("SearchFight"));

            return loadedAssemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterface(typeof(ISearchEngine).ToString()) != null)
                .Select(type => Activator.CreateInstance(type) as ISearchEngine).ToList();
        }
    }
}
