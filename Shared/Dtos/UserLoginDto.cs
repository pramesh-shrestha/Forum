namespace Shared.Dtos; 

public class UserLoginDto {
    public string Username { get; set; }
    public string Password { get; private set; }

    public UserLoginDto(string username, string password) {
        Username = username;
        Password = password;
    }
}