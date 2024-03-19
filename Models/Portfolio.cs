using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finshark_API.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        [Key]
        public int Id { get; set; } 
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}
