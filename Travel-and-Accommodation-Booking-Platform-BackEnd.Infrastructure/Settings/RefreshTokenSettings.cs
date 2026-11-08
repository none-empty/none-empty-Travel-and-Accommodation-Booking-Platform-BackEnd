namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Settings;

public class RefreshTokenSettings
{
    public const string SectionName = nameof(RefreshTokenSettings);
    public int ExpiresInDays { get; set; }
}