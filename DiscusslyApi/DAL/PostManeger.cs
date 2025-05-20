using Discussly.Models;
using DiscusslyApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscusslyApi.DAL
{
    public class PostManeger
    {
        private readonly DiscusslyApiContext _context;

        public PostManeger(DiscusslyApiContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Post.ToListAsync();
        }
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Post.FindAsync(id);
        }
        public async Task AddPostAsync(Post post)
        {
            _context.Post.Add(post);

            // Find the related category and increment PostsCount
            var category = await _context.Category.FindAsync(post.CategoryId);
            if (category != null)
            {
                category.PostsCount++;
            }

            await _context.SaveChangesAsync();
        }
        public async Task UpdatePostAsync(Post post)
        {
            var tracked = await _context.Post.FindAsync(post.Id);
            if (tracked != null)
            {
                // only Updates the these properties 
                tracked.Title = post.Title;
                tracked.Content = post.Content;
                tracked.ImageUrl = post.ImageUrl;
                tracked.UpdatedAt = DateTime.Now;
                tracked.Upvotes = post.Upvotes;
                tracked.Downvotes = post.Downvotes;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeletePostAsync(int id)
        {
            var post = await GetPostByIdAsync(id);
            if (post != null)
            {
                // Find the related category and decrement PostsCount
                var category = await _context.Category.FindAsync(post.CategoryId);
                if (category != null && category.PostsCount > 0)
                {
                    category.PostsCount--;
                }

                _context.Post.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
        
    }
}
