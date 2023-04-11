namespace Shared.Dtos; 

public class ForumPostUpdateDto {
    public int PostId { get; set; }
    public string Username { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Content { get; set; }

    public ForumPostUpdateDto(int postId) {
        PostId = postId;
    }
}