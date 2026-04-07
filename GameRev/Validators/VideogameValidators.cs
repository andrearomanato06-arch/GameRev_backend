namespace GameRev.Validators;

using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.Repository.Entities.Interfaces;

public class VideogameRequestValidators : AbstractValidator<VideogameRequest>
{
    public VideogameRequestValidators(IAuthorRepository authorRepository, IPlatformRepository platformRepository)
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title can't be  null")
            .NotEmpty().WithMessage("Title can't be empty");
        
        RuleFor(x => x.Description)
            .NotNull().WithMessage("Description can't be null")
            .NotEmpty().WithMessage("Description can't be empty");
        
        RuleFor(x => x.CoverImage)
            .NotNull().WithMessage("Cover image can't be null")
            .NotEmpty().WithMessage("Cover image can't be empty");
        
        RuleFor(x => x.Objectives)
            .NotNull().WithMessage("Objectives number can't be null")
            .NotEmpty().WithMessage("Obkectives number can't be empty");
        
        RuleFor(x => x.ReleaseDate)
            .NotNull().WithMessage("Release date can't be  null")
            .NotEmpty().WithMessage("Release date can't be empty");
        
        RuleFor(x => x.Released)
            .NotNull().WithMessage("Released info can't be  null")
            .NotEmpty().WithMessage("Released infos can't be empty");
       
        RuleForEach(x => x.Platforms).SetValidator(new VideogamePlatformValidator(platformRepository));
        
        When(x => x.AuthorId > 0, () =>
        {
            RuleFor(x => x.AuthorId).MustAsync(async (id, CancellationToken) =>
            {
                var author = await authorRepository.GetByIdAsync(id, CancellationToken.None);
                if(author is null) return false;
                return true;
            }).WithMessage("Author not found");
        });
    }
}