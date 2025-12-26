using FluentEmail.Core;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Services;

public class EmailServiceManager : IEmailServiceManager
{
    private readonly IFluentEmail _emailSender;

    public EmailServiceManager(IFluentEmail emailSender)
    {
        _emailSender = emailSender;
    }

    public Task SendEmail(string to, string subject, string body,bool html)
    {
       return _emailSender
            .To(to)
            .Subject(subject)
            .Body(body,isHtml:html)
            .SendAsync();
    }
}