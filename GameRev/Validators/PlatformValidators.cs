using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.Models.Entities;
using GameRev.Repository.Entities.Interfaces;

namespace GameRev.Validators;

public class PlatformValidator : AbstractValidator<PlatformRequest>
{
    public PlatformValidator (IPlatformRepository platformRepository)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Platform name can't be null")
            .NotEmpty().WithMessage("Platform name can't be empty")
            .MustAsync(async (name, ct) =>
            {
                return ! await platformRepository.ExistsByNameAsync(name,ct); 
            }).WithMessage("Platform alredy registered");
    }
}


public class VideogamePlatformValidator : AbstractValidator<Platform>
{
    public VideogamePlatformValidator(IPlatformRepository platformRepository)
    {
        // this is a util for checking Platform object in the videogame request
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Platform name can't be null")
            .NotEmpty().WithMessage("Platform name can't be empty")
            .MustAsync(async (name, ct) =>
            {
                return await platformRepository.ExistsByNameAsync(name,ct);
            }).WithMessage("The platform provided do not exists");
    }
}