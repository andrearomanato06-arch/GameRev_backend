using FluentValidation;
using GameRev.DTOs.Requests.Update;
using GameRev.Models.Utils;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Validators.Utils;

namespace GameRev.Validators;

public class UserValidator : AbstractValidator<UpdateUserRequest>
{
    public UserValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id).MustAsync(async (id,ct) =>
        {
            return await userRepository.ExistsById(id,ct);
        }).WithMessage("User not found for the specified Id");

        When( x=> x.Username is not null, () =>
        {
            RuleFor(x => x.Username).MustAsync(async (username,ct) =>
            {   
                if(username is null) return false;
                return !await userRepository.ExistsByUsername(username,ct);
            }).WithMessage("This username is alredy in use");
        });
        
        When(x => x.Email is not null, () =>
        {
            RuleFor(x => x.Email).IsEmailFormatValid().MustAsync(async (model, email, ct) =>
            {
                var user = await userRepository.GetByEmailAsync(email,ct);
                if(user is null) return true;
                return user.Id == model.Id;
            }).WithMessage("This email may be alredy in use with another account");
        });

        When(x => x.Password is not null, () =>
        {
           RuleFor(x => x.Password).IsPasswordFormatValid(); 
        });

        When(x => x.Role is not null, () =>
        {
            RuleFor(x => x.Role).Must(x => x == UserRole.BASIC || x == UserRole.ADMIN);
        });
    }
}