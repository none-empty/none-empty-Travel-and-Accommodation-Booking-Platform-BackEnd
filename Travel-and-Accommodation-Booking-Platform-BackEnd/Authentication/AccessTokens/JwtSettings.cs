namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Authentication.AccessTokens;

public class JwtSettings
{
    public const string SectionName = nameof(JwtSettings); // Use a constant for the section name
    public string SymmetricSecurityKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiresInMinutes { get; set; }
}