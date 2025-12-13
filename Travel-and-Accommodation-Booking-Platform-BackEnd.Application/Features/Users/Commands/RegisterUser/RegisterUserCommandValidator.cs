using FluentValidation;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
   
    public RegisterUserCommandValidator(IUserRepository userRepository)
    {
        

        RuleFor(c => c.UserName)
            .NotEmpty().WithMessage("User name is required.")
            .Length(3, 50).WithMessage("User name must be between 3 and 50 characters.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.");
        
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email format is required.");
        
         
        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumberIsRequired")
            .Matches(@"^\+?[0-9\s-]{7,20}$").WithMessage("Phone number format is invalid.");


 
        RuleFor(c => c.Email)
            .MustAsync(async (email, cancellation) =>
                !await userRepository.ExistsByEmailAsync(email, cancellation)
            )
            .WithMessage("The specified email address is already in use.");

         
        RuleFor(c => c.UserName)
            .MustAsync(async (userName, cancellation) =>
            
                !await userRepository.ExistsByUserNameAsync(userName, cancellation)
            )
            .WithMessage("The specified username is already registered.");
    }
}