using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Responses;
using GameRev.Models.Auth;
using GameRev.Repository.Auth;
using GameRev.Repository.Auth.Interfaces;
using GameRev.Services.Auth.Interfaces;
using GameRev.Services.Interfaces;

namespace GameRev.Services.Auth;

public class AccessService : IAccessService
{
    private readonly IAccessRepository accessRepository;
    private readonly IUserSessionService userSessionService;

    public AccessService (IAccessRepository accessRepository, IUserSessionService userSessionService)
    {
        this.accessRepository = accessRepository;
        this.userSessionService = userSessionService;
    }

    public async Task<JwtTokenResponse?> Login(LoginRequest request, CancellationToken ct)
    {
        var user = await accessRepository.Login(request,ct);
        if(user is null)
        {
            return null;
        }
        var token = await userSessionService.GenerateJwtToken(user,ct);
        if(token is null)
        {
            return null;
        }

        return new JwtTokenResponse
        {
            Token = token
        };
    }

    public async Task<UserResponse?> Register(UserRequest request, CancellationToken ct)
    {
        var user = DtosToModels.UserRequestToUser(request);
        if(user is null) return null;
        user = await accessRepository.Register(user,ct);
        if(user is null) return null;
        return ModelsToDtos.UserToUserResponse(user);
    }
}