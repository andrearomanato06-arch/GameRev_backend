using GameRev.DTOs.Requests;
using GameRev.DTOs.Responses;

namespace GameRev.Services.Interfaces;

public interface IAccessService
{
    Task<JwtTokenResponse?> Login (LoginRequest request, CancellationToken ct);

    Task<UserResponse?> Register (UserRequest request, CancellationToken ct);
}