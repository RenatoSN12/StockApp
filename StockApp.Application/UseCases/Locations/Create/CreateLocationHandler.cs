using MediatR;
using Microsoft.Extensions.Logging;
using StockApp.Application.DTOs.Responses.Location;
using StockApp.Application.Mappers;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Enums;
using StockApp.Domain.Repositories;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Locations.Create;

public sealed class CreateLocationHandler(ILocationRepository repository, IUnitOfWork unitOfWork , ILogger<CreateLocationHandler> logger)
    : IRequestHandler<CreateLocationCommand, Result<LocationDto>>
{

    public async Task<Result<LocationDto>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newDtoResult =
                Location.Create(request.UserId, request.Dto.Title, request.Dto.Description, EStatus.Active);
            if (newDtoResult.IsFailure)
                return Result.Failure<LocationDto>(newDtoResult.Error);

            await repository.AddAsync(newDtoResult.Value!, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            
            return Result.Success(newDtoResult.Value!.ToDto());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ocorreu um erro inesperado durante a criação da categoria");
            return Result.Failure<LocationDto>(new Error("500",
                "Ocorreu um erro inesperado durante a criação da categoria"));
        }    
    }
}