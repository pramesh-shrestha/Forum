
using System.Security.Claims;
using System.Text.Json;
using BlazorServerApp.Services.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;


namespace BlazorServerApp.Services.Implementations; 

public class UserHttpClient : IUserService {

    private readonly HttpClient client;
    // this private variable for simple caching
    public static string? Jwt { get; private set; } = "";

    //This is an Action, which will fire an event whenever the authentication state changes, i.e. upon log in or log out.
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public UserHttpClient(HttpClient client) {
        this.client = client;
    }
    

    //Register
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

    //Login
    public async Task LoginAsync(UserLoginDto userLoginDto) {
        HttpResponseMessage response = await client.PostAsJsonAsync("api/user/login", userLoginDto);
        string token = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode) {
            throw new Exception(token);
        }

        Jwt = token;

        //Get the ClaimsPrincipal by calling the private method
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        
        //Invoke the Action to notify whoever is interested about the change in authentication state.
        OnAuthStateChanged.Invoke(principal);
    }

    //Logout, clear authentication state and become anonymous
    public Task LogoutAsync() {
        Jwt = null;
        ClaimsPrincipal claimsPrincipal = CreateClaimsPrincipal();
        OnAuthStateChanged.Invoke(claimsPrincipal);
        return Task.CompletedTask;
    }

    //This method is used to provide authentication state details to the Blazor auth framework, whenever the app needs to know about who is logged in.
    public Task<ClaimsPrincipal> GetAuthAsync() {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }
    
    //We will need functionality to convert the JWT received from the Web API into Claims with information about the user logged in.
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
    
    //Method to create ClaimPrincipal. We get the Jwt as a response from the WebApi. We use that Jwt to and create a ClaimPrincipal by extracting claims from Jwt.
    //We need ClaimPrincipal because Blazor framework do not understand custom object, so we must change custom object (user in this case) to ClaimPrincipal.
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
    
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

}