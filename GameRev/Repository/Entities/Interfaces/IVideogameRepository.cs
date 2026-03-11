using GameRev.DTOs.Filters;
using GameRev.Models.Entities;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IVideogameRepository : IGenericCrudRepository<Videogame>
{
    Task<Videogame?> GetByTitleAsync (string title, CancellationToken ct);

    Task<List<Videogame>> GetByPlatformAsync (string platform, CancellationToken ct);

    Task<List<Videogame>> GetNewAsync (CancellationToken ct);

    Task<List<Videogame>> GetMostLikedAsync (int elementsToShow, CancellationToken ct);

    Task<List<Videogame>> SearchAsync (VideogameSearchFilter filter, CancellationToken ct);
}