using Finshark_API.DTOs.Comment;
using Finshark_API.Models;

namespace Finshark_API.Interfaces
{
    public interface ICommentInterface
    {
        Task<List<Comment>> GetCommentsAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> WriteCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
        Task<bool> SaveAsync();

    }
}
