using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EFCDataAccess; 

public class ForumContext : DbContext{

    public DbSet<User> Users { get; set; }
    public DbSet<ForumPost> ForumPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("Data Source = ../EFCDataAccess/Forum.db"); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>().HasKey(user => user.Username);
        modelBuilder.Entity<ForumPost>().HasKey(post => post.PostId); 
    }
}