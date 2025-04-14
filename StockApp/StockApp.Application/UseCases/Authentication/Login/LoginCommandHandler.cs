using MediatR;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Abstractions.Results;
using StockApp.Domain.DTOs.Responses;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Users;

namespace StockApp.Application.UseCases.Authentication.Login;

public class LoginCommandHandler(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    LoginCommandValidator validator)
    : IRequestHandler<LoginCommand, Result<UserDto>>, IValidatableHandler<LoginCommand>
{
    public async Task<Result<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateRequestAsync(request, cancellationToken);
        if (validationResult.IsFailure)
            return Result.Failure<UserDto>(validationResult.Error);

        var spec = new GetUserByEmailSpecification(request.Email);
        var user = await repository.GetUserBySpecificationAsync(spec, cancellationToken);

        if (user == null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            return Result.Failure<UserDto>(new Error("401", "Credenciais inválidas."));

        return Result.Success(new UserDto(user.Fullname.FirstName, user.Fullname.LastName, user.Email));
    }
    
    public async Task<Result> ValidateRequestAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        return !result.IsValid
            ? Result.Failure(new Error("400", string.Join("\n", result.Errors.Select(e => e.ErrorMessage))))
            : Result.Success();
    }
}