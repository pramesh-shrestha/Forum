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

    public Task<ForumPost> CreatePostAsync(ForumPost post) {
        throw new NotImplementedException();
    }

    public Task<ForumPost> GetPostByIdAsync(int postId) {
        throw new NotImplementedException();
    }

    public Task<List<ForumPost>> GetAllPostsAsync() {
        throw new NotImplementedException();
    }

    public Task DeletePostByIdAsync(int postId) {
        throw new NotImplementedException();
    }

    public Task UpdatePostAsync(ForumPostDto postDto) {
        throw new NotImplementedException();
    }

    public Task<ForumPost> GetPostByUsernameAsync(string postDtoUsername) {
        throw new NotImplementedException();
    }
}