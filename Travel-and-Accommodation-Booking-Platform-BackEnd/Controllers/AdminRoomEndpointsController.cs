using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/Rooms")]
[Authorize(Policy = "Admin Only")]
public class AdminRoomEndpointsController : ControllerBase
{
    private readonly IRepository<Room> _roomRpository;
    private readonly IGuidGenerator _guidGenerator;

    public AdminRoomEndpointsController(IRepository<Room> roomRepository,IGuidGenerator guidGenerator)
    {
        _roomRpository = roomRepository;
        _guidGenerator = guidGenerator;
    }

    [HttpDelete("{id}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteRoom(Guid id)
    {
        await _roomRpository.DeleteByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }
    
    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRoom([FromBody]AddRoomRequest request)
    {
        var room = new Room
        {
           RoomId = _guidGenerator.Generate(),
           HotelId = request.HotelId,
           AdultsCapacity = request.AdultsCapacity,
           ChildrenCapacity = request.ChildrenCapacity,
           Description = request.Description,
           RoomNumber = request.RoomNumber,
           Status = request.Status
        };

        await _roomRpository.AddAsync(room);
        return StatusCode(StatusCodes.Status201Created,room.RoomId);
    }
    
    [HttpPut] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRoom([FromBody]UpdateRoomRequest request)
    {
        var room =await _roomRpository.GetByIdAsync(request.RoomId);

        if (room is null) return StatusCode(StatusCodes.Status400BadRequest);

        room = UpdateRoomInfo(request,room);
        
        await _roomRpository.UpdateAsync(room);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }

    private Room UpdateRoomInfo(UpdateRoomRequest request,Room room)
    {
        room.RoomId = request.RoomId;
        room.HotelId = request.HotelId;
        room.AdultsCapacity = request.AdultsCapacity;
        room.ChildrenCapacity = request.ChildrenCapacity;
        room.Description = request.Description;
        room.RoomNumber = request.RoomNumber;
        room.Status = request.Status;
        return room;
    }
}