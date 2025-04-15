using MediatR;
using StockApp.Application.Abstractions.Security;
using StockApp.Application.DTOs.Responses.Authentication;
using StockApp.Application.UseCases.Abstractions;
using StockApp.Domain.Abstractions.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Enums;
using StockApp.Domain.Repositories;
using StockApp.Domain.Specification.Users;
using StockApp.Domain.ValueObjects;
using StockApp.Shared;

namespace StockApp.Application.UseCases.Authentication.Register;

public class RegisterCommandHandler(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    RegisterCommandValidator validator)
    : HandlerWithValidation<RegisterCommand>(validator),IRequestHandler<RegisterCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateRequestAsync(request, cancellationToken);
        if (!validationResult.IsSuccess)
            return Result.Failure<UserDto>(validationResult.Error);

        var fullnameResult = Fullname.Create(request.FirstName, request.LastName);
        if (!fullnameResult.IsSuccess || fullnameResult.Value is null)
            return Result.Failure<UserDto>(fullnameResult.Error);

        var user = new User
        {
            Fullname = fullnameResult.Value,
            Email = request.Email,
            PasswordHash = passwordHasher.HashPassword(request.Password),
            IsActive = EStatus.Active
        };

        await repository.AddAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Create(new UserDto(user.Fullname.FirstName, user.Fullname.LastName, user.Email));
    }

    private async Task<bool> EmailExists(string email, CancellationToken cancellationToken)
    {
        var spec = new GetUserByEmailSpecification(email);
        var user = await repository.GetUserBySpecificationAsync(spec, cancellationToken);

        return user != null;
    }

    public override async Task<Result> ValidateRequestAsync(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
            return Result.Failure(new Error("400",
                string.Join(".", result.Errors.Select(x => x.ErrorMessage))));

        if (await EmailExists(request.Email, cancellationToken))
            return Result.Failure(new Error("400", "E-mail já está em uso."));

        return Result.Success();
    }
}