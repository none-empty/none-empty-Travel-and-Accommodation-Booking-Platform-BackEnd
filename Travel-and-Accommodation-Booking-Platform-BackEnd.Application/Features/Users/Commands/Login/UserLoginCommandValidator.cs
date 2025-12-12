using FluentValidation;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Login;

public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginCommandValidator(IUserRepository userRepository,IPasswordHasher hasher)
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email format is required.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required.");

    }
}