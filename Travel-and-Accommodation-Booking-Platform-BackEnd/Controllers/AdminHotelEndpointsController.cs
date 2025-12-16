using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/Hotels")]
[Authorize(Policy = "Admin Only")]
public class AdminHotelEndpointsController : ControllerBase
{
     
    private readonly IGuidGenerator _guidGenerator;
    private readonly IHotelRepository _hotelRepository;

    public AdminHotelEndpointsController(IHotelRepository hotelRepository,IGuidGenerator guidGenerator)
    {
        _hotelRepository = hotelRepository;
        _guidGenerator = guidGenerator;
    }

    [HttpDelete("{id}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteHotel(Guid id)
    {
        await _hotelRepository.DeleteByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }
    
    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddHotel([FromBody]AddHotelRequest request)
    {
        var hotel = new Hotel
        {
            HotelId = _guidGenerator.Generate(),
            Address = request.Address,
            Category = request.Category,
            CityId = request.CityId,
            Description = request.Description,
            HotelName = request.HotelName,
            Owner = request.Owner,
            PricePerNight = request.PricePerNight,
            PhoneNumber = request.PhoneNumber,
            StarRating = request.StarRating
        };

        await _hotelRepository.AddAsync(hotel);
        return StatusCode(StatusCodes.Status201Created,hotel.HotelId);
    }
    
    [HttpPut] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateHotel([FromBody]UpdateHotelRequest request)
    {
        var hotel =await _hotelRepository.GetByIdAsync(request.HotelId);

        if (hotel is null) return StatusCode(StatusCodes.Status400BadRequest);

        hotel = UpdateHotelInfo(request,hotel);
        
        await _hotelRepository.UpdateAsync(hotel);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }

    private Hotel UpdateHotelInfo(UpdateHotelRequest request, Hotel hotel)
    {
        hotel.HotelId = request.HotelId;
        hotel.Address = request.Address;
        hotel.Category = request.Category;
        hotel.CityId = request.CityId;
        hotel.Description = request.Description;
        hotel.HotelName = request.HotelName;
        hotel.Owner = request.Owner;
        hotel.PricePerNight = request.PricePerNight;
        hotel.PhoneNumber = request.PhoneNumber;
        hotel.StarRating = request.StarRating;
        
        return hotel;
    }
}