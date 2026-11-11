namespace Powdernaut.Domain.UseCases.Security.Parameters;

public record AppUserUpdateParameters(
    Guid IdEntity,
    string Name,
    string UserName,
    string Email,
    string PhoneNumber);
