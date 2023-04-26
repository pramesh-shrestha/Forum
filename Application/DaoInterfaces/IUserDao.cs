﻿
using Shared.Models;

namespace Application.DaoInterfaces; 

public interface IUserDao {
    Task<User> RegisterAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
}