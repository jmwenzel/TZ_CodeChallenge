namespace SearchFight.BL.Models
{
    public class GoogleResult
    {
        public SearchInformation SearchInformation { get; set; }    
    }

    public class SearchInformation
    {
        public string TotalResults { get; set; }
    }
}
