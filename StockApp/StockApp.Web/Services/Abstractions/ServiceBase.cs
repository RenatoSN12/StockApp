using System.Net.Http.Json;
using StockApp.Shared;

namespace StockApp.Web.Services.Abstractions;

public abstract class ServiceBase
{
    protected async Task<Result<T>> SendRequestAsync<T>(Func<Task<HttpResponseMessage>> requestFunc,
        CancellationToken cancellationToken)
    {
        var response = await requestFunc();

        if (!response.IsSuccessStatusCode)
            return await ErrorManager.CreateTypedFailureFromResponse<T>(response);
        
        var data = await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        return Result.Success(data!);
    }
}