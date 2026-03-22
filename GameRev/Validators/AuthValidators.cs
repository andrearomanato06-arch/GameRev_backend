namespace GameRev.Validators;

using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Validators.Utils;

public class RegistrationValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email).IsEmailFormatValid().MustAsync(async (email,ct) =>
        {
            return ! await userRepository.ExistsByEmail(email,ct);
        }).WithMessage("The email provided for registration is alredy registered");

        RuleFor(x => x.Password).IsPasswordFormatValid();

        RuleFor(x => x.Username).NotEmpty().MustAsync(async (username, ct) =>
        {
            return !await userRepository.ExistsByUsername(username,ct);    
        }).WithMessage("This username is not available");
    }
}

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email).IsEmailFormatValid();
        RuleFor(x => x.Password).IsPasswordFormatValid();
    }
}