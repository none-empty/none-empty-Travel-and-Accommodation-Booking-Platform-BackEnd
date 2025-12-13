using System.ComponentModel.DataAnnotations;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

public class UserRegistrationRequest
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; init; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 8)]

    public string Password { get; init; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; init; } = null!;

    [Phone] [StringLength(20)] 
    public string PhoneNumber { get; init; } = null!;
}