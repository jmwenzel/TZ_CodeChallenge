using System.Configuration;

namespace SearchFight.BL.Config
{
    public class BingConfig
    {
        public static string BaseUrl => ConfigurationManager.AppSettings["Bing.BaseUrl"];
        public static string ApiKey => ConfigurationManager.AppSettings["Bing.ApiKey"];
    }
}
