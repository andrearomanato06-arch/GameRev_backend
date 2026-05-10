using GameRev.Models.Entities;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IPlatformRepository : IGenericCrudRepository<Platform>
{
    Task<Platform?> GetByNameAsync (string name, CancellationToken ct);

    Task <bool> ExistsByNameAsync (string name, CancellationToken ct);

    Task<bool> ExistsById (long id, CancellationToken ct);

    Task<List<Platform>> GetByIds(List<long> platformsIds);
}