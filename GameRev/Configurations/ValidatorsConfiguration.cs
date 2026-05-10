using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.Models.Entities;
using GameRev.Validators;

namespace GameRev.Configurations;

public static class ValidatorsConfiguration
{
    public static IServiceCollection AddRequestsValidators(this IServiceCollection service)
    {
        
        service.AddScoped<IValidator<UserRequest>, UserValidator>();
        service.AddScoped<IValidator<UpdateUserRequest>, UpdateUserValidator>();
        service.AddScoped<IValidator<AuthorRequest>, AuthorValidator>();
        service.AddScoped<IValidator<VideogameRequest>, VideogameRequestValidators>();
        service.AddScoped<IValidator<ReviewRequest>, ReviewValidators>();
        service.AddScoped<IValidator<PlatformRequest>, PlatformValidator>();
        //service.AddScoped<IValidator<Platform>, VideogamePlatformValidator>();

        service.AddScoped<IValidator<RegistrationRequest>, RegistrationValidator>();
        service.AddScoped<IValidator<LoginRequest>, LoginValidator>();
        return service;
    }
}