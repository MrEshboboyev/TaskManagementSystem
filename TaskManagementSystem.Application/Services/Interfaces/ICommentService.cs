using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Comment;

namespace TaskManagementSystem.Application.Services.Interfaces;

public interface ICommentService
{
    // Add a new comment to a task
    Task<ResponseDTO<CommentDTO>> AddCommentAsync(CommentCreateDTO commentDto);

    // Update an existing comment
    Task<ResponseDTO<CommentDTO>> UpdateCommentAsync(int commentId, CommentUpdateDTO commentDto);

    // Get all comments for a specific task
    Task<ResponseDTO<IEnumerable<CommentDTO>>> GetCommentsForTaskAsync(int taskId);

    // Get a specific comment by ID
    Task<ResponseDTO<CommentDTO>> GetCommentByIdAsync(int commentId);

    // Delete a comment
    Task<ResponseDTO<bool>> DeleteCommentAsync(CommentDeleteDTO commentDeleteDTO);
}