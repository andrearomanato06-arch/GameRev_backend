using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
using GameRev.Models.Entities;
using GameRev.Models.Utils;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Entities.Interfaces;

namespace GameRev.Services.Entities;

public class UserService : IUserService
{

    private readonly IUserRepository userRepository;
    private readonly ILogger<UserService> logger;

    public UserService (IUserRepository userRepository, ILogger<UserService> logger)
    {
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public async Task<UserResponse?> AddAsync(UserRequest request, CancellationToken ct)
    {
        var user = DtosToModels.UserRequestToUser(request);
        var response = await userRepository.AddAsync(user,ct);
        return response is not null 
        ? ModelsToDtos.UserToUserResponse(response)
        : null;
    }

    public async Task<List<UserResponse>> GetAllAsync(CancellationToken ct)
    {
        List<User> users = await userRepository.GetAllAsync(ct);
        return ModelsToDtos.UserToUserResponse(users); 
    }

    public async Task<UserResponse?> GetByEmailAsync(string email, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(email,ct);
        return user is not null
        ? ModelsToDtos.UserToUserResponse(user)
        : null;
    }

    public async Task<UserResponse?> GetByIdAsync(long id, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(id,ct);
        return user is not null
        ? ModelsToDtos.UserToUserResponse(user)
        : null;    
    }

    public async Task<UserResponse?> GetByUsernameAsync(string username, CancellationToken ct)
    {
        var user = await userRepository.GetByUsernameAsync(username,ct);
        return user is not null
        ? ModelsToDtos.UserToUserResponse(user)
        : null;
    }

    public async Task<bool> RemoveAsync(long id, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(id,ct);
        if(user is null)
        {
            logger.LogWarning("Failed to find user with ID: ${Id}", id);
            return false;
        }
        return await userRepository.DeleteAsync(user,ct);
    }

    public async Task<bool> UpdateAsync(UpdateUserRequest request, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(request.Id, ct);
        if(user is null)
        {
            logger.LogWarning("Failed to find user with ID: ${Id}", request.Id);
            return false;
        }
        UpdateModel.UpdateUserFromDto(user,request);
        return await userRepository.UpdateAsync(user,ct);
    }
}