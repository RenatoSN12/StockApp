using System.Net;
using System.Text.Json;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Results;
using StockApp.Web.Extensions;

namespace StockApp.Web.Services;

public static class ErrorManager
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        IncludeFields = true,
    };
    private static async Task<ErrorReturn> ExtractErrorResponse (HttpResponseMessage response)
    {
        try
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            
            if (string.IsNullOrWhiteSpace(errorContent))
            {
                return new ErrorReturn
                {
                    Code = response.StatusCode.StringParse(),
                    Message = "Falha na requisição. Ocorreu um erro inesperado."
                };
            }

            var errorResponse = JsonSerializer.Deserialize<ErrorReturn>(errorContent, JsonOptions);

            return errorResponse ?? new ErrorReturn
            {
                Code = response.StatusCode.StringParse(),
                Message = "Erro desconhecido. Falha ao processar a resposta do servidor."
            };
        }
        catch (Exception e)
        {
            return new ErrorReturn
            {
                Code = response.StatusCode.StringParse(),
                Message = $"Erro ao processar a resposta do servidor: {e.Message}"
            };
        }
    }
    public static async Task<Result> ErrorResponse(HttpResponseMessage response)
    {
        var errorResponse = await ExtractErrorResponse(response);
        return Result.Failure(new Error(errorResponse.Code, errorResponse.Message));
    }
    
    public static async Task<Result<T>> ErrorPagedResponse<T>(HttpResponseMessage response)
    {
        var errorResponse = await ExtractErrorResponse(response);
        return Result<T>.Failure(new Error(errorResponse.Code, errorResponse.Message));
    }
}