using Finshark_API.Data;
using Finshark_API.DTOs.Comment;
using Finshark_API.Interfaces;
using Finshark_API.Mappers;
using Finshark_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Finshark_API.Repository
{
    public class CommentRepository : ICommentInterface
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comments = await _context.comments.Where(x => x.Id == id).FirstOrDefaultAsync();
            return comments;

        }
        public async Task<List<Comment>> GetCommentsAsync()
        {
            var comments = await _context.comments.ToListAsync();

            return comments;
        }

        public async Task<Comment> WriteCommentAsync(Comment comment)
        {
            await _context.comments.AddAsync(comment);
            await SaveAsync();
            return comment;
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            if (result < 0) return false;
            return true;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _context.comments.FirstOrDefaultAsync(s => s.Id == id);
            if(comment  == null) return false;
            _context.Remove(comment);
            return await SaveAsync(); 
        }
    }
}
