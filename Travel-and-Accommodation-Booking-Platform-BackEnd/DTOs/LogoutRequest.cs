using System.ComponentModel.DataAnnotations;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

public record LogoutRequest([property: Required] string RefreshToken);
