namespace TaskManagementSystem.Application.Common.Models;

public class CreateCommentModel
{
    public int TaskId { get; set; }
    public string Content { get; set; }
}