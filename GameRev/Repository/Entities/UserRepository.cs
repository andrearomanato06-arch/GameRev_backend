using GameRev.Data;
using GameRev.Models.Entities;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameRev.Repository.Entities;

public class UserRepository : GenericCrudRepository<User>, IUserRepository
{

    public UserRepository(AppDbContext context) : base (context){}

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email), ct);
    }

    public async Task<User?> GetByUsernameAsync (string username, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username), ct);
    }

    public async Task<bool> ExistsByUsername (string username, CancellationToken ct)
    {
        return await context.Users.Where(u => u.Username.Equals(username)).FirstOrDefaultAsync(ct) is not null;
    }

    public async Task<bool> ExistsByEmail (string email, CancellationToken ct)
    {
        return await context.Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync(ct) is not null;
    }

    public async Task<bool> ExistsById (long id, CancellationToken ct)
    {
        return await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(ct) is not null;
    }
}