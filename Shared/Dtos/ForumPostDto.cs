using Shared.Models;

namespace Shared.Dtos; 

public class ForumPostDto {
    public User User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}