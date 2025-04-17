using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.DTOs.Requests.Products;
using StockApp.Application.DTOs.Responses.Products;
using StockApp.Application.UseCases.Products.Create;
using StockApp.Application.UseCases.Products.GetAll;
using StockApp.Application.UseCases.Products.GetByCustomId;
using StockApp.Application.UseCases.Products.Update;
using StockApp.Shared;

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

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<List<ResumeProductDto>>>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var userId = HttpContext.GetUserEmail();
        var query = new GetAllProductsQuery(userId, pageNumber, pageSize);
        
        var result = await sender.Send(query);

        return result.ToActionResult();
    }
    
    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> GetByCustomId(string productId)
    {
        var userId = HttpContext.GetUserEmail();
        var query = new GetProductByCustomIdQuery(userId, productId);
        
        var result = await sender.Send(query);

        return result.ToActionResult();
    }
    
    [HttpPut]
    public async Task<ActionResult<ProductDto>> Update(ProductDto dto)
    {
        
        var userId = HttpContext.GetUserEmail();
        var query = new UpdateProductCommand(dto, userId);
        
        var result = await sender.Send(query);

        return result.ToActionResult();
    }
    
}