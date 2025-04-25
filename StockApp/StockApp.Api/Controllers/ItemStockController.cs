using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.DTOs.Requests.ProductStock;
using StockApp.Application.DTOs.Responses.ProductStock;
using StockApp.Application.UseCases.ProductStock.Create;
using StockApp.Application.UseCases.ProductStock.Delete;

namespace StockApp.Api.Controllers;

[ApiController]
[Route("api/product-stock")]
[Authorize]
public class ItemStockController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task <ActionResult<ProductStockDto>> Post(CreateProductStockDto dto)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new CreateProductStockCommand(userId, dto);
        
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
    
    [HttpDelete]
    public async Task <ActionResult<string>> Delete(DeleteProductStockDto dto)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new DeleteProductStockCommand(userId, dto);
        
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
}