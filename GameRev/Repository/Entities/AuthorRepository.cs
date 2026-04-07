namespace GameRev.Repository.Entities;

using GameRev.Repository.Generic;
using GameRev.Models.Entities;
using GameRev.Data;
using GameRev.Repository.Entities.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

public class AuthorRepository : GenericCrudRepository<Author>, IAuthorRepository
{
    public AuthorRepository(AppDbContext context) : base(context) {}

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct)
    {
        return await context.Authors.AnyAsync(a => a.Name.Equals(name), ct);
    }
}