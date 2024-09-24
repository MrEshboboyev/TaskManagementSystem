using TaskManagementSystem.Application.DTOs;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface ICommentService
{
    // Add a new comment to a task
    Task<CommentDTO> AddCommentAsync(CommentCreateDTO commentDto);

    // Update an existing comment
    Task<CommentDTO> UpdateCommentAsync(int commentId, CommentUpdateDTO commentDto);

    // Get all comments for a specific task
    Task<IEnumerable<CommentDTO>> GetCommentsForTaskAsync(int taskId);

    // Get a specific comment by ID
    Task<CommentDTO> GetCommentByIdAsync(int commentId);

    // Delete a comment
    Task DeleteCommentAsync(int commentId);
}