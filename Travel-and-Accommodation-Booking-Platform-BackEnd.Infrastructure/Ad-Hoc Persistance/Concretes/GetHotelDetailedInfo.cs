using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.Queries;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class GetHotelDetailedInfo : IGetHotelDetailedInfo
{
    private readonly AppDbContext _context;
    
    public GetHotelDetailedInfo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetHotelDetailedInfoQueryResponse> Execute(Guid id)
    {

        var query = await _context.Database.SqlQuery<HotelPrimaryInfo>(
            $@"SELECT TOP(1) h.HotelId,h.HotelName,c.Name as CityName,h.StarRating,h.ThumbnailUrl,h.Category
                   , h.PricePerNight,h.Description,h.Address,h.PhoneNumber
                   FROM Hotels h inner join Cities c on h.CityId = c.CityId
                   WHERE h.HotelId = {id}").ToListAsync();

        var hotelInfo = query.FirstOrDefault();
         

        var hotelImages = await _context.HotelsImages
            .Where(image => image.HotelId.Equals(id)).Select(image => image.ImageUrl).ToListAsync();

        var hotelReviews = await _context.HotelsReviews
            .Include(review => review.User)
            .Where(hotel => hotel.HotelId.Equals(id))
            .Select(review => new HotelReviewInfo(review.User!.UserName, review.Review)).ToListAsync();

        var hotelRooms = await _context.Rooms
            .Where(room => room.HotelId.Equals(id))
            .Select(room => new HotelRoomInfo(room.RoomId,room.RoomNumber, room.AdultsCapacity
                , room.ChildrenCapacity, room.Description)).ToListAsync();



        return new GetHotelDetailedInfoQueryResponse(hotelInfo!, hotelRooms, hotelReviews, hotelImages);
    }
}