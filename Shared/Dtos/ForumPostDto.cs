using Shared.Models;

namespace Shared.Dtos; 

public class ForumPostDto {
    public int PostId { get; set; }
    public string Username { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Content { get; set; }

    public ForumPostDto(int postId,string username, string title, string category, string content) {
        PostId = postId;
        Username = username;
        Title = title;
        Category = category;
        Content = content;
    }
}   