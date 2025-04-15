using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockApp.Shared;

namespace StockApp.Api.Common.Extensions;

public static class ResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        var statusCode = int.TryParse(result.Error.Code, out var code)
            ? code
            : 400;

        return new ObjectResult(result.Error) { StatusCode = statusCode};
    }
    
    public static ActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new ObjectResult(new {success = true, code = 200})
            {
                StatusCode = 200,
            };

        var statusCode = int.TryParse(result.Error.Code, out var code)
            ? code
            : 400;

        return new ObjectResult(result.Error) { StatusCode = statusCode};
    }
}