
using System.Text.Json;
using BlazorServerApp.Services.ClientInterfaces;

using Shared.Models;


namespace BlazorServerApp.Services.Implementations; 

public class UserHttpClient : IUserService {

    private readonly HttpClient client;

    public UserHttpClient(HttpClient client) {
        this.client = client;
    }

    public async Task<User> RegisterUser(User user) {
        HttpResponseMessage response = await client.PostAsJsonAsync("api/user/register", user);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception(result);
        }

        User newUser = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;

        return newUser;
    }
}