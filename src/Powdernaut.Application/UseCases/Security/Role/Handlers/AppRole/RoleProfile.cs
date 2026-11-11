using Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetAll;
using Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetById;
using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<AppRoleEntity, RoleGetAllResponse>().ReverseMap();
        CreateMap<AppRoleEntity, RoleGetByIdResponse>().ReverseMap();
    }
}
