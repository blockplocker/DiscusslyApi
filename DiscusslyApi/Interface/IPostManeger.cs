using Discussly.Models;

namespace DiscusslyApi.Interface
{
    public interface IPostManeger
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }
}
