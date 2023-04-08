
using System.Security.Claims;
using Shared.Dtos;
using Shared.Models;

namespace BlazorServerApp.Services.ClientInterfaces; 

public interface IUserService {
    Task<User> RegisterUser(User user);
    public Task LoginAsync(UserLoginDto userLoginDto);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }

}