using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.Queries;

public record GetHotelDetailedInfoQuery(Guid Id) : IRequest<GetHotelDetailedInfoQueryResponse>;
 

public record GetHotelDetailedInfoQueryResponse(HotelPrimaryInfo HotelPrimaryInfo,List<HotelRoomInfo>HotelRooms
    ,List<HotelReviewInfo>HotelReviews,List<string>HotelImagesUrls);

public record HotelPrimaryInfo(
    Guid HotelId,
    string HotelName,
    string CityName,
    int StarRating,
    string? ThumbnailUrl,
    HotelCategory Category,
    decimal PricePerNight,
    string? Description,
    string Address,
    string PhoneNumber);

public record HotelRoomInfo(Guid RoomId,int RoomNumber, int AdultsCapacity, int ChildrenCapacity, string? Description);

public record HotelReviewInfo(string UserName,string Review);
 
