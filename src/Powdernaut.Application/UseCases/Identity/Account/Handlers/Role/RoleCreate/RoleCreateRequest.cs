namespace Powdernaut.Application.UseCases.Identity.Account.Handlers.Role.RoleCreate;

public class RoleCreateRequest : RequestModel<RoleCreateResponse>
{
    public string Name { get; set; } = string.Empty;
}
