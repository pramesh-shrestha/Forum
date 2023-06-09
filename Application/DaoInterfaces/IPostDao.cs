﻿using Shared.Models;

namespace Application.DaoInterfaces; 

public interface IPostDao {
    Task<ForumPost> CreatePostAsync(ForumPost post);
    Task<ForumPost> GetPostByIdAsync(int postId);
    Task<List<ForumPost>> GetAllPostsAsync();
    Task DeletePostAsync(ForumPost post);
    Task UpdatePostAsync(ForumPost post);
  
}