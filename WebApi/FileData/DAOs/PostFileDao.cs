using FileData;
using Shared.Dtos;
using Shared.Models;
using WebApi.FileData.DaoInterfaces;

namespace WebApi.FileData.DAOs; 

public class PostFileDao : IPostDao {

    private FileContext context;

    public PostFileDao(FileContext context) {
        this.context = context;
    }

    //create post
    public Task<ForumPost> CreatePostAsync(ForumPost post) {
        int id = 1;
        //finding the last id that was given
        if (context.Posts.Any()) {
            id = context.Posts.Max(p => post.PostId);
            id++;
        }

        post.PostId = id;
        context.Posts.Add(post);
        context.SaveChanges();
        return Task.FromResult(post);
    }

    //get post by id
    public Task<ForumPost> GetPostByIdAsync(int postId) {
        ForumPost post = context.Posts.FirstOrDefault(p => p.PostId == postId)!;
        return Task.FromResult(post);
    }

    //get all posts
    public Task<List<ForumPost>> GetAllPostsAsync() {
        List <ForumPost> posts = context.Posts.ToList();
        return Task.FromResult(posts);
    }

    //delete post
    public Task DeletePostAsync(ForumPost post) {
        context.Posts.Remove(post);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    //update post
    public Task UpdatePostAsync(ForumPost post) {
        ForumPost existingPost = context.Posts.FirstOrDefault(p => p.PostId == post.PostId)!;
        context.Posts.Remove(existingPost);
        context.Posts.Add(post);
        context.SaveChanges();
        return Task.CompletedTask;
    }
    
}