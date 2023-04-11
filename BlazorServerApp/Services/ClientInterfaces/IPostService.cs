using Shared.Dtos;
using Shared.Models;

namespace BlazorServerApp.Services.ClientInterfaces; 

public interface IPostService {
    Task<ForumPost> CreatePostAsync(ForumPostDto postDto);
    Task<List<ForumPost>> GetAllPostsAsync();
    Task<ForumPost> GetPostByIdAsync(int postId);
}