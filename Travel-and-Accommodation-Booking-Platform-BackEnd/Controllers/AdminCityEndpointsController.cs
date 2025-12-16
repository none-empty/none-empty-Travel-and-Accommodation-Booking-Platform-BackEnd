using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/Cities")]
[Authorize(Policy = "Admin Only")]
public class AdminCityEndpointsController : ControllerBase
{
    private readonly IRepository<City> _cityRpository;
    private readonly IGuidGenerator _guidGenerator;

    public AdminCityEndpointsController(IRepository<City> cityRepository,IGuidGenerator guidGenerator)
    {
        _cityRpository = cityRepository;
        _guidGenerator = guidGenerator;
    }

    [HttpDelete("{id}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCity(Guid id)
    {
        await _cityRpository.DeleteByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }
    
    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCity([FromBody]AddCityRequest request)
    {
        var city = new City
        {
            CityId = _guidGenerator.Generate(),
            Country = request.Country,
            Name = request.Name,
            PostOffice = request.PostOffice,
            NumberOfHotels = request.NumberOfHotels,
            ThumbnailUrl = request.ThumbnailUrl
        };

        await _cityRpository.AddAsync(city);
        return StatusCode(StatusCodes.Status201Created,city.CityId);
    }
    
    [HttpPut] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCity([FromBody]UpdateCityRequest request)
    {
        var city =await _cityRpository.GetByIdAsync(request.CityId);

        if (city is null) return StatusCode(StatusCodes.Status400BadRequest);

        city = UpdateCityInfo(request,city);
        
        await _cityRpository.UpdateAsync(city);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }

    private City UpdateCityInfo(UpdateCityRequest request, City city)
    {
        city.Country = request.Country;
        city.Name = request.Name;
        city.NumberOfHotels = request.NumberOfHotels;
        city.ThumbnailUrl = request.ThumbnailUrl;
        city.PostOffice = request.PostOffice;
        return city;
    }
}