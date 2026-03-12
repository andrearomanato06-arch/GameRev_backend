using GameRev.DTOs.Requests;
using GameRev.DTOs.Responses;
using GameRev.Models.Entities;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Entities.Interfaces;

namespace GameRev.Services.Entities;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository authorRepository;

    public AuthorService (IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<AuthorResponse?> AddAsync(AuthorRequest request, CancellationToken ct)
    {
        var author = new Author {Name = request.Name};
        var response = await authorRepository.AddAsync(author,ct);
        return response is not null
        ? new AuthorResponse
        {
            Id = response.Id,
            Name = response.Name
        }
        : null;
    }

    public async Task<List<AuthorResponse>> GetAllAsync(CancellationToken ct)
    {
        var authors = await authorRepository.GetAllAsync(ct);
        List<AuthorResponse> response = [];
        foreach(Author a in authors)
        {
            response.Add(new AuthorResponse
            {
                Id = a.Id,
                Name = a.Name,
            });
        }
        return response;
    }

    public async Task<AuthorResponse?> GetByIdAsync(long id, CancellationToken ct)
    {
        var response = await authorRepository.GetByIdAsync(id,ct);
        return response is not null ?
        new AuthorResponse
        {
            Id = response.Id,
            Name = response.Name
        }
        : null;
    }

    public async Task<bool> RemoveAsync(long id, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(id,ct);
        if(author is null) return false;
        return await authorRepository.DeleteAsync(author,ct);
    }

    public async Task<bool> UpdateAsync(UpdateAuthorRequest request, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(request.Id,ct);
        if(author is null) return false;
        author.Name = request.Name;
        return await authorRepository.UpdateAsync(author,ct);
    }
}