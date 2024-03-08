namespace Finshark_API.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } 
        public bool IsDecending { get; set; }

    }
}
