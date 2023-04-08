namespace Shared.Models; 

public class ForumPost {
    public int PostId { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}