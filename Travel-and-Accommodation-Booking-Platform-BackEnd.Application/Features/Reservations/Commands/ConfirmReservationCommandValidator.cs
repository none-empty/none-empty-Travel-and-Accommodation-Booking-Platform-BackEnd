using FluentValidation;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Reservations.Commands;

public class ConfirmReservationCommandValidator : AbstractValidator<ConfirmReservationCommand>
{
    public ConfirmReservationCommandValidator(IDateGetter dateGetter, IGetRoomCapacities getRoomCapacities)
    {
        RuleFor(c => c.HotelId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.PaymentMethod).IsInEnum();

        RuleFor(c => c.CheckInDate)
            .Must(date => date.Date >= dateGetter.GetUtcNow().Date)
            .WithMessage("Check-in date cannot be in the past.");

        RuleFor(c => c.CheckOutDate)
            .Must((command, date) => date.Date > command.CheckInDate.Date)
            .WithMessage("Check-out date must be at least one calendar day after check-in.");

        RuleFor(c => c.RoomsData)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("At least one room is required.")
            .Must(rooms => rooms.All(room => room is not null))
            .WithMessage("Room entries cannot be null.")
            .Must(rooms => rooms.Select(room => room.RoomId).Distinct().Count() == rooms.Count)
            .WithMessage("Each room can only be selected once.")
            .DependentRules(() =>
            {
                RuleForEach(c => c.RoomsData).ChildRules(room =>
                {
                    room.RuleFor(r => r.RoomId).NotEmpty();
                    room.RuleFor(r => r.NumberOfAdults).GreaterThanOrEqualTo(0);
                    room.RuleFor(r => r.NumberOfChildren).GreaterThanOrEqualTo(0);
                    room.RuleFor(r => r)
                        .Must(r => r.NumberOfAdults > 0 || r.NumberOfChildren > 0)
                        .WithMessage("Each room must have at least one guest.");
                }).DependentRules(() =>
                {
                    RuleFor(c => c).CustomAsync(async (command, context, cancellationToken) =>
                    {
                        var capacities = await getRoomCapacities.Execute(
                            command.RoomsData.Select(room => room.RoomId), cancellationToken);

                        for (var index = 0; index < command.RoomsData.Count; index++)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            var requestedRoom = command.RoomsData[index];
                            if (!capacities.TryGetValue(requestedRoom.RoomId, out var room))
                            {
                                context.AddFailure($"RoomsData[{index}].RoomId", "The selected room does not exist.");
                                continue;
                            }

                            if (requestedRoom.NumberOfAdults > room.AdultsCapacity)
                                context.AddFailure($"RoomsData[{index}].NumberOfAdults", "The number of adults exceeds the room's capacity.");
                            if (requestedRoom.NumberOfChildren > room.ChildrenCapacity)
                                context.AddFailure($"RoomsData[{index}].NumberOfChildren", "The number of children exceeds the room's capacity.");
                        }
                    });
                });
            });
    }
}
