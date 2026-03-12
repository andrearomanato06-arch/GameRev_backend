using GameRev.DTOs.Requests;
using GameRev.DTOs.Responses;
using GameRev.Models.Entities;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Interfaces;

namespace GameRev.Services.Entities;

public class PlatformService : IPlatformService
{

    private readonly IPlatformRepository platformRepository;

    public PlatformService(IPlatformRepository platformRepository)
    {
        this.platformRepository = platformRepository;
    }

    public async Task<PlatformResponse?> AddAsync(PlatformRequest request, CancellationToken ct)
    {
        var platform = new Platform {Name = request.Name};
        var response = await platformRepository.AddAsync(platform,ct);
        return response is not null 
        ? new PlatformResponse
        {
            Id = response.Id,
            Name = response.Name
        }
        : null;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var platform = await platformRepository.GetByIdAsync(id,ct);
        if(platform is null) return false;
        return await platformRepository.DeleteAsync(platform,ct);
    }

    public async Task<List<PlatformResponse>> GetAllAsync(CancellationToken ct)
    {
        var platforms = await platformRepository.GetAllAsync(ct);
        List<PlatformResponse> response = [];
        foreach(Platform platform in platforms)
        {
            response.Add(new PlatformResponse
            {
               Id = platform.Id,
               Name = platform.Name 
            });
        }
        return response;
    }

    public async Task<PlatformResponse?> GetByIdAsync(long id, CancellationToken ct)
    {
        var response = await platformRepository.GetByIdAsync(id,ct);
        return response is not null
        ? new PlatformResponse
        {
            Id = response.Id,
            Name = response.Name
        }
        : null;
    }

    public async Task<PlatformResponse?> GetByNameAsync(string name, CancellationToken ct)
    {
        var response = await platformRepository.GetByNameAsync(name,ct);
        return response is not null
        ? new PlatformResponse
        {
            Id = response.Id,
            Name = response.Name
        }
        : null;
    }

    public async Task<bool> UpdatePlatformAsync(UpdatePlatformRequest request, CancellationToken ct)
    {
        var platform = await platformRepository.GetByIdAsync(request.Id,ct);
        if(platform is null) return false;
        platform.Name = request.Name;
        return await platformRepository.UpdateAsync(platform,ct);
    }
}