using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Application.UseCases.Categories.Create;
using StockApp.Application.UseCases.Categories.Delete;
using StockApp.Application.UseCases.Categories.GetAll;
using StockApp.Application.UseCases.Categories.Update;

namespace StockApp.Api.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoryController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var userId = HttpContext.GetUserEmail();
        var query = new GetAllCategoriesQuery(userId, pageNumber, pageSize);
        var result = await sender.Send(query);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto request)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new CreateCategoryCommand(userId, request);
        
        var result = await sender.Send(command);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new DeleteCategoryCommand(userId,id);
        
        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Ok()
            : BadRequest(result.Error);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto request)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new UpdateCategoryCommand(request, id, userId);
        
        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
    
}