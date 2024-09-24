using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Repositories;

public class CommentRepository(AppDbContext db) : Repository<Comment>(db), ICommentRepository
{
    private readonly AppDbContext _db = db;

    public async Task Update(Comment comment)
    {
        _db.Comments.Update(comment);
    }
}
