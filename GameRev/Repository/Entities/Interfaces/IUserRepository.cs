using GameRev.Models.Entities;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IUserRepository : IGenericCrudRepository<User>
{
    Task<User?> GetByEmailAsync (string email, CancellationToken ct);

    Task<User?> GetByUsernameAsync (string username, CancellationToken ct);
}