namespace Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetById;

public class RoleGetByIdResponse : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
