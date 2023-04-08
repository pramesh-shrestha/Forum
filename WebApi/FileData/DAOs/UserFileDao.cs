
using FileData;
using Shared.Models;
using WebApi.FileData.DaoInterfaces;

namespace WebApi.FileData.DAOs; 

public class UserFileDao : IUserDao {

    private readonly FileContext context;

    public UserFileDao(FileContext context) {
        this.context = context;
    }

    //registering the user
    public async Task<User> RegisterAsync(User user) {
        context.Users.Add(user);
        await Task.Run((() => context.SaveChanges()));
        return user;
    }

    //getting the user by username
    public Task<User?> GetByUsernameAsync(string userName) {
        User? user = context.Users.FirstOrDefault(u => u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(user);
    }
}   