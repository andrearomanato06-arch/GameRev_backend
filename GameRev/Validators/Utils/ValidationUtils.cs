namespace GameRev.Validators.Utils;

using FluentValidation;

public static class ValidationUtils
{
    public static IRuleBuilderOptions<T, string> IsEmailFormatValid<T> (this IRuleBuilder<T,string?> ruleBuilder)
        => ruleBuilder
        .NotNull().WithMessage("Null emails are not allowed")
        .NotEmpty().WithMessage("Email is required")
        .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid email format");
    
    public static IRuleBuilder <T,string> IsPasswordFormatValid<T> (this IRuleBuilder<T, string?> ruleBuilder)
        => ruleBuilder
        .NotNull().WithMessage("Null passwords are not allowed")
        .NotEmpty().WithMessage("Password is required")
        .MinimumLength(8).WithMessage("Password must be at least 9 characters long")
        .Matches(@"^(?=.*[!@#$%^&*(),.? ""':{}|<>\[\]\\]).{8,}$").WithMessage("Password must contain at least one special character");
}