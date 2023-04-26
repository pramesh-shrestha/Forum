
using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EFCDataAccess.DAOs; 

public class PostDaoImpl : IPostDao {

    private readonly ForumContext context;

    public PostDaoImpl(ForumContext context) {
        this.context = context;
    }

    public async Task<ForumPost> CreatePostAsync(ForumPost post) {
        EntityEntry<ForumPost> newPost = await context.ForumPosts.AddAsync(post);
        await context.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<ForumPost> GetPostByIdAsync(int postId) {
        ForumPost? post = await context.ForumPosts.FindAsync(postId);
        if (post == null) {
            throw new Exception($"Post with post Id {postId} does not exist");
        }

        return post;
    }

    public async Task<List<ForumPost>> GetAllPostsAsync() {
        return await context.ForumPosts.ToListAsync();
    }

    public async Task DeletePostAsync(ForumPost post) {
        context.ForumPosts.Remove(post);
        await context.SaveChangesAsync();
    }   

    public async Task UpdatePostAsync(ForumPost post) {
        context.ForumPosts.Update(post);
        await context.SaveChangesAsync();
    }
}