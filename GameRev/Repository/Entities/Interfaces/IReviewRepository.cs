using GameRev.Models.Entities;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IReviewRepository : IGenericCrudRepository<Review>
{
    Task<List<Review>> GetGameReviewsAsync (long gameId, CancellationToken ct);

    Task<List<Review>> GetUserReviewsAsync (long userId, CancellationToken ct);
}