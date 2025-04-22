using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Common.Extensions;
using StockApp.Application.DTOs.Requests.ItemStock;
using StockApp.Application.DTOs.Responses.ItemStock;
using StockApp.Application.UseCases.ItemStock.Create;
using StockApp.Application.UseCases.ItemStock.Delete;

namespace StockApp.Api.Controllers;

[ApiController]
[Authorize]
[Route("/item-stock")]
public class ItemStockController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task <ActionResult<ItemStockDto>> Post(CreateItemStockDto dto)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new CreateItemStockCommand(userId, dto);
        
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
    
    [HttpDelete]
    public async Task <ActionResult<ItemStockDto>> Delete(DeleteItemStockDto dto)
    {
        var userId = HttpContext.GetUserEmail();
        var command = new DeleteItemStockCommand(userId, dto);
        
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
}