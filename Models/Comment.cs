namespace Finshark_API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public int? StockId { get; set; }
        // Navigation property - allows us to navigate inside our model
        public Stock? Stock { get; set; }
    }
}
