using Microsoft.EntityFrameworkCore;
using GameRev.Data;
using GameRev.Models.Entities;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities;

public class PlatformRepository : GenericCrudRepository<Platform>, IPlatformRepository
{
    public PlatformRepository(AppDbContext context) : base(context) {}
    public async Task<Platform?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await context.Platforms.FirstOrDefaultAsync(p => p.Name.Equals(name),ct);
    }

    public async Task<bool> ExistsByNameAsync (string name, CancellationToken ct)
    {
        return await context.Platforms.AnyAsync(p => p.Name.Equals(name), ct);
    }

    public async Task<bool> ExistsById (long id, CancellationToken ct)
    {
        return await context.Platforms.AnyAsync(p => p.Id == id,ct);
    }

    public async Task<List<Platform>> GetByIds(List<long> platformsIds)
    {
        List<Platform> platforms = [];
        foreach(long id in platformsIds)
        {
            var x = await context.Platforms.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(x is not null)
                platforms.Add(x);
        }
        return platforms;
    }
}