namespace Powdernaut.Domain.UseCases.Security.Parameters;

public record AppUserCreateParameters(
    string Name,
    string UserName,
    string Email,
    string PhoneNumber);
