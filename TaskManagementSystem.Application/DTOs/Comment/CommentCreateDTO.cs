namespace TaskManagementSystem.Application.DTOs.Comment;

public class CommentCreateDTO
{
    public int TaskId { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
}
