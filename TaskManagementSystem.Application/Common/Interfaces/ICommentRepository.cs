using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Common.Interfaces;

public interface ICommentRepository : IRepository<Comment>
{
     Task Update(Comment comment);  
}

