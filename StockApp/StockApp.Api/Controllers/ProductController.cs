using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.DTOs.Requests.Products;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Products.Create;
using StockApp.Domain.Specification.Items;

namespace StockApp.Api.Controllers;


[ApiController]
[Route("api/products")]
[Authorize]
public class ProductController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto createProductDto)
    {
        var userId = HttpContext.GetUserEmail();

        var command = new CreateProductCommand(createProductDto, userId);
        
        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    // [HttpGet]
    // public async Task<ActionResult<ProductDto>> GetAll()
    // {
    //     var userId = HttpContext.GetUserEmail();
    //     var specification = new Command(userId);
    // }
}