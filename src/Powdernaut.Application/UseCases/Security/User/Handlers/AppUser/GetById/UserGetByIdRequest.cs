namespace Powdernaut.Application.UseCases.Security.User.Handlers.AppUser.GetById;

public class UserGetByIdRequest : RequestModel<UserGetByIdResponse>
{
    public UserGetByIdRequest(Guid entityId)
    {
        EntityId = entityId;
    }

    public Guid EntityId { get; set; }
}
