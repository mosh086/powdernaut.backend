using Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;
using Powdernaut.Application.UseCases.Security.User.Handlers.AppUser.GetAll;
using Powdernaut.Application.UseCases.Security.User.Handlers.AppUser.GetById;
using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Application.UseCases.Security.User.Handlers.AppUser;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AppUserEntity, UserGetAllResponse>().ReverseMap();
        CreateMap<AppUserEntity, UserGetByIdResponse>().ReverseMap();
        CreateMap<UserRegisterRequest, AppUserEntity>().ReverseMap();

        CreateMap<AppUserEntity, UserRegisterRequest>()
            .ReverseMap();
    }
}