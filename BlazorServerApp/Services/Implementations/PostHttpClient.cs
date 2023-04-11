using System.Net.Http.Headers;
using System.Text.Json;
using BlazorServerApp.Services.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace BlazorServerApp.Services.Implementations; 

public class PostHttpClient : IPostService {

    private HttpClient client;
    public string? Jwt { get; set; }

    public PostHttpClient(HttpClient client) {
        this.client = client;
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Jwt);
    }

    public async Task<ForumPost> CreatePostAsync(ForumPostDto postDto) {
        LoadClientWithToken();
        HttpResponseMessage response = await client.PostAsJsonAsync($"/api/post/createPost", postDto);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception(result);
        }

        ForumPost post = JsonSerializer.Deserialize<ForumPost>(result, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    public async Task<List<ForumPost>> GetAllPostsAsync() {
        LoadClientWithToken();
        HttpResponseMessage response = await client.GetAsync("api/post");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) {
            throw new Exception(result);
        }
        
        List<ForumPost> posts = JsonSerializer.Deserialize<List<ForumPost>>(result, new JsonSerializerOptions {
           PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<ForumPost> GetPostByIdAsync(int postId) {
        LoadClientWithToken();
        HttpResponseMessage response = await client.GetAsync($"api/post/{postId}");
        string result = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode) {
            throw new Exception(result);
        }

        ForumPost post = JsonSerializer.Deserialize<ForumPost>(result, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    private void LoadClientWithToken() {
        Jwt =  UserHttpClient.Jwt;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Jwt);
    }
}           