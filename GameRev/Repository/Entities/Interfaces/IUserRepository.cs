using GameRev.Models.Entities;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IUserRepository : IGenericCrudRepository<User>
{
    Task<User?> GetByEmailAsync (string email, CancellationToken ct);

    Task<User?> GetByUsernameAsync (string username, CancellationToken ct);

    Task<bool> ExistsByUsername (string username, CancellationToken ct);
    
    Task<bool> ExistsByEmail (string email, CancellationToken ct);

    Task<bool> ExistsById (long id, CancellationToken ct);
}