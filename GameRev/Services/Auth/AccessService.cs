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
    private readonly ILogger<AccessService> logger;

    public AccessService (IAccessRepository accessRepository, IUserSessionService userSessionService, ILogger<AccessService> logger)
    {
        this.accessRepository = accessRepository;
        this.userSessionService = userSessionService;
        this.logger = logger;
    }

    public async Task<JwtTokenResponse?> Login(LoginRequest request, CancellationToken ct)
    {
        var user = await accessRepository.Login(request,ct);
        if(user is null)
        {
            logger.LogWarning("Invalid credentials");
            return null;
        }
        var token = await userSessionService.GenerateJwtToken(user,ct);
        if(token is null)
        {
            logger.LogWarning("Cannot generate the JWT token");
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
        if(user is null) {
            logger.LogError("Error while parsing from dto to model (UserRequest -> User)");
            return null;
        }
        user = await accessRepository.Register(user,ct);
        if(user is null) {
            logger.LogWarning("Failed to register new user");
            return null;
        }
        return ModelsToDtos.UserToUserResponse(user);
    }
}