using Discussly.Models;
using DiscusslyApi.Data;
using DiscusslyApi.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace DiscusslyApi.DAL
{
    public class CommentManeger : ICommentManeger
    {
        private readonly DiscusslyApiContext _context;

        public CommentManeger(DiscusslyApiContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comment.ToListAsync();
        }
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comment.FindAsync(id);
        }
        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCommentAsync(Comment comment)
        {
            var tracked = await _context.Post.FindAsync(comment.Id);
            if (tracked != null)
            {
                // only Updates the these properties 
                tracked.Content = comment.Content;
                tracked.UpdatedAt = DateTime.Now;
                tracked.Upvotes = comment.Upvotes;
                tracked.Downvotes = comment.Downvotes;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCommentAsync(int id)
        {
            var comment = await GetCommentByIdAsync(id);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
