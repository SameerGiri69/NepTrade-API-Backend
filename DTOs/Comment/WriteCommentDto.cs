using System.ComponentModel.DataAnnotations;

namespace Finshark_API.DTOs.Comment
{
    public class WriteCommentDto
    {
        [Required]
        [MaxLength(280,ErrorMessage ="Title cannot exceed 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(280, ErrorMessage = "Content cannot exceed 280 characters")]
        public string Content { get; set; } = string.Empty;
       
    }
}
