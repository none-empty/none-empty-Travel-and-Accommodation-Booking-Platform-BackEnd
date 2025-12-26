namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

public interface IEmailServiceManager
{
    Task SendEmail(string to,string subject,string body,bool html);
}