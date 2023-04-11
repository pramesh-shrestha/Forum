using Shared.Dtos;
using Shared.Models;

namespace WebApi.Services.LogicInterfaces; 

public interface IPostLogic {
    Task<ForumPost> CreatePostAsync(ForumPostDto postDto);
    Task<ForumPost> GetPostByIdAsync(int postId);
    Task<List<ForumPost>> GetAllPostsAsync();   
    Task DeletePostByIdAsync(int postId);
    Task UpdatePostAsync(ForumPostUpdateDto postDto);
}   