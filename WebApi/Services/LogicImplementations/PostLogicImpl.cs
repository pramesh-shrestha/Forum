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

    //create post
    public async Task<ForumPost> CreatePostAsync(ForumPostDto postDto) {
        Validate(postDto);
        ForumPost post = new ForumPost {
            Username = postDto.Username,
            Title = postDto.Title,
            Category = postDto.Category,
            Content = postDto.Content
        };
        return await postDao.CreatePostAsync(post);
        
    }

    //validate
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

    //get post by id
    public async Task<ForumPost> GetPostByIdAsync(int postId) {
        ForumPost post = await postDao.GetPostByIdAsync(postId);
        if (post == null) {
            throw new Exception($"Post not found with id {postId}");
        }

        return post;
    }

    //get all posts
    public async Task<List<ForumPost>> GetAllPostsAsync() {
        return await postDao.GetAllPostsAsync();
    }

    //delete
    public async Task DeletePostByIdAsync(int postId) {
        ForumPost post = await postDao.GetPostByIdAsync(postId);
        if (post == null) {
            throw new Exception($"No post found with id {postId}");
        }

        await postDao.DeletePostAsync(post);
    }

    //update
    public async Task UpdatePostAsync(ForumPostUpdateDto postDto) {
    ForumPost existingPost = await postDao.GetPostByIdAsync(postDto.PostId);
    if (existingPost == null) {
        throw new Exception("Post not found");
    }
    string username = postDto.Username;
    string title = postDto.Title ?? existingPost.Title; //take postDto.Title. In case if it is null, then take existingPost.Title
    string category = postDto.Category ?? existingPost.Category;
    string content = postDto.Content ?? existingPost.Content;
    
    ForumPost newPost = new ForumPost {
        Username = username,
        Title = title,
        Category = category,
        Content = content
    };
    
    await postDao.UpdatePostAsync(newPost);
    }
    
    
}