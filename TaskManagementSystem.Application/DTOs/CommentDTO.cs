namespace TaskManagementSystem.Application.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserFullName { get; set; }
    public string TaskTitle { get; set; }
}

