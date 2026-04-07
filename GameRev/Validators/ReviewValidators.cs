using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.Repository.Entities.Interfaces;

namespace GameRev.Validators;

public class ReviewValidators : AbstractValidator<ReviewRequest>
{
    public ReviewValidators(IUserRepository userRepository, IVideogameRepository videogameRepository)
    {
       
        When(x => x.Rating is not null, () =>
        {
            RuleFor(r => (double) r.Rating).Must(IsRatingInBounds);
        });
            
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("You can't put an empty description");
        
        RuleFor(x => x.ReviewDate)
            .NotNull().WithMessage("Review date cen't be null")
            .NotEmpty().WithMessage("Review date cen't be empty")
            .LessThanOrEqualTo(DateTime.Now);
        
        RuleFor(x => x.VideogameId)
            .NotNull().WithMessage("Videogame ID can't  be null")
            .NotEmpty().WithMessage("Videogame ID can't be empty")
            .MustAsync(async (videogameId, ct) =>
            {
                return await videogameRepository.ExistsByIdAsync(videogameId, ct);
            }).WithMessage("The videogame ID provided don't exists");
        
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID can't be null")
            .NotEmpty().WithMessage("User ID can't be empty")
            .MustAsync(async (userId, ct) =>
            {
                return ! await userRepository.ExistsById(userId, ct);
            }).WithMessage("Cannot find the specified user ID");
    }

    private static bool IsRatingInBounds (double rating)
    {
        return rating >= 0.0 && rating <= 5.0;
    }
}