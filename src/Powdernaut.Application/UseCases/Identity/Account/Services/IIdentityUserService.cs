using Microsoft.AspNetCore.Identity;
using Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;

namespace Powdernaut.Application.UseCases.Identity.Account.Services;

public interface IIdentityUserService
{
    Task<IdentityResult> Register(UserRegisterRequest parameter);
}
