namespace Shared.Models; 

public class ForumPost {
    public int PostId { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Content { get; set; }

    public ForumPost(User user, string title, string category, string content) {
        User = user;
        Title = title;
        Category = category;
        Content = content;
    }
}