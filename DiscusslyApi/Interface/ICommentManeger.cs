using Discussly.Models;

namespace DiscusslyApi.Interface
{
    public interface ICommentManeger
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }
}
