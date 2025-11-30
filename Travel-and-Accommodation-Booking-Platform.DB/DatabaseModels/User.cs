namespace Travel_and_Accommodation_Booking_Platform.DB.DatabaseModels;

public class User
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;


    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}