using Microsoft.AspNetCore.Identity;
using Powdernaut.Application.UseCases.Identity.Account.Handlers.Role.RoleCreate;

namespace Powdernaut.Application.UseCases.Identity.Account.Services;

public interface IIdentityRoleService
{
    Task<IdentityResult> CreateAsync(RoleCreateRequest parameter);
}
