using Microsoft.Extensions.Options;
using Shared.Dtos;
using Shared.Models;
using WebApi.FileData.DaoInterfaces;
using WebApi.Services.LogicInterfaces;

namespace WebApi.Services.LogicImplementations; 

public class PostLogicImpl : IPostLogic {

    private IPostDao postDao;

    public PostLogicImpl(IPostDao postDao) {
        this.postDao = postDao;
    }

    public async Task<ForumPost> CreatePostAsync(ForumPostDto postDto) {

        Validate(postDto);
        ForumPost post = new ForumPost(new User(postDto.Username), postDto.Title, postDto.Category, postDto.Content);
        return await postDao.CreatePostAsync(post);
        
    }

    private void Validate(ForumPostDto postDto) {
        if (string.IsNullOrEmpty(postDto.Title)) {
            throw new Exception("Title cannot be empty");
        }
        if (string.IsNullOrEmpty(postDto.Category)) {
            throw new Exception("Category must be selected");
        }
        if (string.IsNullOrEmpty(postDto.Content)) {
            throw new Exception("Content cannot be empty");
        }
    }

    public async Task<ForumPost> GetPostByIdAsync(int postId) {
        ForumPost post = await postDao.GetPostByIdAsync(postId);
        if (post == null) {
            throw new Exception($"Post not found with id {postId}");
        }

        return post;
    }

    public async Task<List<ForumPost>> GetAllPostsAsync() {
        List<ForumPost> posts = await postDao.GetAllPostsAsync();
        return posts;
    }

    public async Task DeletePostByIdAsync(int postId) {
        await postDao.DeletePostByIdAsync(postId);
    }

    public async Task UpdatePostAsync(ForumPostDto postDto) {
        ForumPost existingPost = await postDao.GetPostByUsernameAsync(postDto.Username);
        string username = postDto.Username;
        string title = postDto.Title ?? existingPost.Title; //take postDto.Title. In case if it is null, then take existingPost.Title
        string category = postDto.Category ?? existingPost.Category;
        string content = postDto.Content ?? existingPost.Content;

        ForumPostDto newPostDto = new ForumPostDto(username, title, category, content);
        await postDao.UpdatePostAsync(newPostDto);
    }
    
    
}