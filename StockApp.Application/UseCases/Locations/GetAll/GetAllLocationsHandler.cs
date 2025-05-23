using MediatR;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.Mappers;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Locations;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Locations.GetAll;

public sealed class GetAllLocationsHandler(ILocationRepository repository)
    : IRequestHandler<GetAllLocationsQuery, Result<PagedResponse<IEnumerable<LocationDto>>>>
{
    public async Task<Result<PagedResponse<IEnumerable<LocationDto>>>> Handle(GetAllLocationsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var spec = new GetAllLocationsSpecification(request.UserId);
            var locations = await repository.GetAllLocationsAsync(spec, cancellationToken);

            var listDto = locations
                .Select(x => x.ToDto());

            var totalCount = await repository.GetTotalCount(spec, cancellationToken);

            return Result<PagedResponse<IEnumerable<LocationDto>>>.Success(
                new PagedResponse<IEnumerable<LocationDto>>(listDto, totalCount, request.PageNumber, request.PageSize));
        }
        catch (Exception e)
        {
            return Result.Failure<PagedResponse<IEnumerable<LocationDto>>>(
                new Error("500", "Ocorreu um erro inesperado ao consultar os locais de estoque."));
        }
    }
}