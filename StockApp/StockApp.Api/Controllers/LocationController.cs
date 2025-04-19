using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.DTOs.Requests.Location;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.UseCases.Locations.Create;
using StockApp.Application.UseCases.Locations.GetAll;
using StockApp.Shared;

namespace StockApp.Api.Controllers;

[Route("api/locations")]
[ApiController]
[Authorize]
public class LocationController(ISender handler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResponse<IEnumerable<LocationDto>>>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var userId = HttpContext.GetUserEmail();
        
        var command = new GetAllLocationsQuery(userId, pageNumber, pageSize);
        
        var result = await handler.Send(command);

        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<LocationDto>> Post(CreateLocationDto request)
    {
        var userId = HttpContext.GetUserEmail();

        var command = new CreateLocationCommand(userId, request);
        
        var result = await handler.Send(command);
        
        return result.ToActionResult();
    }
}