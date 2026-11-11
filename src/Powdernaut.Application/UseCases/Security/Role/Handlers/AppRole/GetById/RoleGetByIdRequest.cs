namespace Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetById;

public class RoleGetByIdRequest : RequestModel<RoleGetByIdResponse>
{
    public RoleGetByIdRequest(Guid entityId)
    {
        EntityId = entityId;
    }

    public Guid EntityId { get; set; }
}