using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EFCDataAccess.DAOs;

public class UserDaoImpl : IUserDao {

    private readonly ForumContext context;

    public UserDaoImpl(ForumContext context) {
        this.context = context;
    }

    public async Task<User> RegisterAsync(User user) {
        Console.WriteLine("I am here");
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName) {
        User? existingUser = await context.Users.FindAsync(userName);
        return existingUser;
    }
}