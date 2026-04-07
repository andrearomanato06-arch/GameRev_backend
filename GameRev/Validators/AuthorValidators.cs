

using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.Repository.Entities.Interfaces;

namespace GameRev.Validators;

public class AuthorValidator : AbstractValidator<AuthorRequest>
{
    public AuthorValidator(IAuthorRepository authorRepository)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Author name can't be null")
            .NotEmpty().WithMessage("Author name can't be empty")
            .MustAsync(async (name, ct) =>
            {
                return ! await authorRepository.ExistsByNameAsync(name,ct);
            }).WithMessage("This author is alredy registered");
    }
}