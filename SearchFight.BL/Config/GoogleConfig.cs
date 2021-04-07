using System.Configuration;

namespace SearchFight.BL.Config
{
    public class GoogleConfig
    {
        public static string BaseUrl => ConfigurationManager.AppSettings["Google.BaseUrl"];
        public static string ApiKey => ConfigurationManager.AppSettings["Google.ApiKey"];
        public static string ContextId => ConfigurationManager.AppSettings["Google.ContextId"];
    }
}
