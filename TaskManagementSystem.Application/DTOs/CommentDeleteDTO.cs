namespace TaskManagementSystem.Application.DTOs;

public class CommentDeleteDTO
{
    public int CommentId { get; set; }
    public int TaskId { get; set; }
    public string UserId { get; set; }
}

