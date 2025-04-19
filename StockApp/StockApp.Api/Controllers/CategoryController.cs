using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.DTOs.Requests.Categories;
using StockApp.Application.DTOs.Responses.Categories;
using StockApp.Application.UseCases.Categories.Create;
using StockApp.Application.UseCases.Categories.Delete;
using StockApp.Application.UseCases.Categories.GetAll;
using StockApp.Application.UseCases.Categories.Update;
using StockApp.Shared;

namespace StockApp.Api.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize]
public class CategoryController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResponse<List<CategoryDto>>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var userId = HttpContext.GetUserEmail();
        var query = new GetAllCategoriesQuery(userId, pageNumber, pageSize);
        var result = await sender.Send(query);

        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto request)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new CreateCategoryCommand(userId, request);
        
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new DeleteCategoryCommandQuery(userId,id);
        
        var result = await sender.Send(command);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] UpdateCategoryDto request)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new UpdateCategoryQuery(request, id, userId);
        
        var result = await sender.Send(command);

        return result.ToActionResult();
    }
    
}