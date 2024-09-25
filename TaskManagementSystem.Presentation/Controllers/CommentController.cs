using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.DTOs.Comment;
using TaskManagementSystem.Application.Services.Interfaces;

namespace TaskManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        private readonly ICommentService _commentService = commentService;

        #region Private Methods

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Login required!");
        #endregion

        [AllowAnonymous]
        [HttpGet("get-comments-for-task")]
        public async Task<IActionResult> GetCommentsForTask(int taskId)
        {
            var result = await _commentService.GetCommentsForTaskAsync(taskId);

            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("get-comment-by-id")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            var result = await _commentService.GetCommentByIdAsync(commentId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("add-comment")]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentModel createCommentModel)
        {
            CommentCreateDTO commentCreateDTO = new()
            {
                Content = createCommentModel.Content,
                TaskId = createCommentModel.TaskId,
                UserId = GetUserId(),
            };

            var result = await _commentService.AddCommentAsync(commentCreateDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentModel updateCommentModel)
        {
            // prepare
            CommentUpdateDTO commentUpdateDTO = new()
            {
                Content = updateCommentModel.Content,
                UserId = GetUserId()
            };

            var result = await _commentService.UpdateCommentAsync(commentId, commentUpdateDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("delete-Comment")]
        public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentModel deleteCommentModel)
        {
            // prepare
            CommentDeleteDTO commentDeleteDTO = new()
            {
                CommentId = deleteCommentModel.CommentId,
                TaskId = deleteCommentModel.TaskId,
                UserId = GetUserId()
            };

            var result = await _commentService.DeleteCommentAsync(commentDeleteDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
