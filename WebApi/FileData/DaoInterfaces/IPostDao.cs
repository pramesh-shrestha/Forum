using Shared.Dtos;
using Shared.Models;

namespace WebApi.FileData.DaoInterfaces; 

public interface IPostDao {
    Task<ForumPost> CreatePostAsync(ForumPost post);
    Task<ForumPost> GetPostByIdAsync(int postId);
    Task<List<ForumPost>> GetAllPostsAsync();
    Task DeletePostByIdAsync(int postId);
    Task UpdatePostAsync(ForumPostDto postDto);
    Task<ForumPost> GetPostByUsernameAsync(string postDtoUsername);
}