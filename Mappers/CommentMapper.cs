using Finshark_API.DTOs.Comment;
using Finshark_API.Models;
using System.Runtime.CompilerServices;

namespace Finshark_API.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDtoFromComment(this Comment comment)
        {
            return new CommentDto()
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                Title = comment.Title,
                StockId = comment.StockId,
                CreatedBy = comment.AppUser.UserName
            };
        }
        public static Comment ToCommentFromWrite(this WriteCommentDto comment, int stockId, AppUser appUser)
        {
            return new Comment()
            {
                Content = comment.Content,
                Title = comment.Title,
                StockId = stockId,
                AppUserId = appUser.Id
                
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentDto comment)
        {
            return new Comment()
            {
                Content = comment.Content,
                Title = comment.Title
            };
        }
    }
}
