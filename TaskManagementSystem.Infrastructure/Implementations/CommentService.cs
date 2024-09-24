using AutoMapper;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Services.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Implementations;

public class CommentService(IUnitOfWork unitOfWork, IMapper mapper) : ICommentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;


    public async Task<ResponseDTO<IEnumerable<CommentDTO>>> GetCommentsForTaskAsync(int taskId)
    {
        try
        {
            // getting all comments for this task
            var commentsFromDb = await _unitOfWork.Comment.GetAllAsync(
                filter: c => c.TaskId.Equals(taskId),
                includeProperties: "User,Task"
                );

            // mapping and return
            return new ResponseDTO<IEnumerable<CommentDTO>>(
                _mapper.Map<IEnumerable<CommentDTO>>(commentsFromDb));
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<CommentDTO>>(ex.Message);
        }
    }

    public async Task<ResponseDTO<CommentDTO>> GetCommentByIdAsync(int commentId)
    {
        try
        {
            // get this comment 
            var commentFromDb = await _unitOfWork.Comment.GetAsync(
                filter: c => c.Id.Equals(commentId),
                includeProperties: "User,Task"
                );

            // mapping and return
            return new ResponseDTO<CommentDTO>(
                _mapper.Map<CommentDTO>(commentFromDb));
        }
        catch (Exception ex)
        {
            return new ResponseDTO<CommentDTO>(ex.Message);
        }
    }


    public async Task<ResponseDTO<CommentDTO>> AddCommentAsync(CommentCreateDTO commentCreateDTO)
    {
        try
        {
            // getting this task
            var taskFromDb = await _unitOfWork.TaskItem.GetAsync(
                t => t.Id.Equals(commentCreateDTO.TaskId)
                ) ?? throw new Exception("Task not found!");

            // prepare task with this taskId
            var commentForDb = _mapper.Map<Comment>(commentCreateDTO);

            // adding to db and save
            await _unitOfWork.Comment.AddAsync(commentForDb);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<CommentDTO>(
                _mapper.Map<CommentDTO>(commentForDb));
        }
        catch (Exception ex)
        {
            return new ResponseDTO<CommentDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<CommentDTO>> UpdateCommentAsync(int commentId, CommentUpdateDTO commentUpdateDTO)
    {
        try
        {
            // getting this comment 
            var commentFromDB = await _unitOfWork.Comment.GetAsync(
                filter: c => c.Id.Equals(commentId) &&
                c.TaskId.Equals(commentUpdateDTO.TaskId) && 
                c.UserId.Equals(commentUpdateDTO.UserId),
                includeProperties: "User,Task"
                ) ?? throw new Exception("Comment not found!");

            // prepare task with this data
            _mapper.Map(commentUpdateDTO, commentFromDB);

            // adding to db and save
            await _unitOfWork.Comment.Update(commentFromDB);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<CommentDTO>(
                _mapper.Map<CommentDTO>(commentFromDB));
        }
        catch (Exception ex)
        {
            return new ResponseDTO<CommentDTO>(ex.Message);
        }
    }

    public async Task<ResponseDTO<bool>> DeleteCommentAsync(CommentDeleteDTO commentDeleteDTO)
    {
        try
        {
            // getting this comment 
            var commentFromDB = await _unitOfWork.Comment.GetAsync(
                c => c.Id.Equals(commentDeleteDTO.CommentId) &&
                c.TaskId.Equals(commentDeleteDTO.TaskId) &&
                c.UserId.Equals(commentDeleteDTO.UserId)
                ) ?? throw new Exception("Comment not found!");

            // remove from db and save
            await _unitOfWork.Comment.RemoveAsync(commentFromDB);
            await _unitOfWork.SaveAsync();

            // mapping and return
            return new ResponseDTO<bool>(
                _mapper.Map<bool>(true));
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>(ex.Message);
        }
    }
}

