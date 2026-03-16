using GameRev.DTOs.Requests;
using GameRev.Models.Entities;

namespace GameRev.Repository.Auth.Interfaces;

public interface IAccessRepository
{
    Task<User?> Login (LoginRequest request, CancellationToken ct);

    Task<User?> Register (User user, CancellationToken ct);
}