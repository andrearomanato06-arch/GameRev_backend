using GameRev.Data;
using GameRev.DTOs.Requests;
using GameRev.Models.Entities;
using GameRev.Repository.Auth.Interfaces;
using GameRev.Repository.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameRev.Repository.Auth;

public class AccessRepository : IAccessRepository
{

    private readonly AppDbContext context;
    private readonly IUserRepository userRepository;
    
    public AccessRepository (AppDbContext context, IUserRepository userRepository)
    {
        this.userRepository = userRepository;
        this.context = context;
    }

    public async Task<User?> Login(LoginRequest request, CancellationToken ct)
    {
        var user = await context.Users.Where(u => u.Email.Equals(request.Email) && u.Password.Equals(request.Password)).FirstOrDefaultAsync(ct);
        if(user is null) return null;
        return user;
    }

    public async Task<User?> Register(User user, CancellationToken ct)
    {
        return await userRepository.AddAsync(user,ct);
    }
}