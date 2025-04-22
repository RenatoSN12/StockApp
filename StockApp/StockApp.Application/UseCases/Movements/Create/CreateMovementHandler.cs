// using MediatR;
// using StockApp.Application.DTOs.Requests.Movements;
// using StockApp.Application.DTOs.Responses.Movement;
// using StockApp.Application.UseCases.Abstractions;
// using StockApp.Domain.Abstractions.Interfaces;
// using StockApp.Domain.Repositories;
// using StockApp.Shared;
//
// namespace StockApp.Application.UseCases.Movements.Create;
//
// public sealed class CreateMovementHandler(IMovementRepository repository, IUnitOfWork unitOfWork, CreateMovementValidator validator) 
//     : HandlerWithValidation<CreateMovementDto>(validator), IRequestHandler<CreateMovementCommand, Result<MovementDto>>
// {
//     public async Task<Result<MovementDto>> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
//     {
//         // var validationResult = await ValidateRequestAsync(request.Dto, cancellationToken);
//         // if (validationResult.IsFailure)
//         //     return Result.Failure<MovementDto>(validationResult.Error);
//         //
//         // return Result<MovementDto>.Success(new MovementDto());
//     }
// }