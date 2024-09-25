namespace TaskManagementSystem.Application.DTOs.Comment;

public class CommentDeleteDTO
{
    public int CommentId { get; set; }
    public int TaskId { get; set; }
    public string UserId { get; set; }
}

