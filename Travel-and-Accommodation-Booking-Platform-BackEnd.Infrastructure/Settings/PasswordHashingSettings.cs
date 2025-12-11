namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Settings;

public class PasswordHashingSettings
{
    public const string SectionName = nameof(PasswordHashingSettings);
    public string Key { get; set; } = null!;
}