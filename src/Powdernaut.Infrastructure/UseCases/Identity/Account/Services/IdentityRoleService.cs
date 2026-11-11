using Microsoft.AspNetCore.Identity.Data;
using Powdernaut.Application.Providers;
using Powdernaut.Application.UseCases.Identity.Account.Handlers.Role.RoleCreate;
using Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;
using Powdernaut.Application.UseCases.Identity.Account.Services;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Repositories;

namespace Powdernaut.Infrastructure.UseCases.Identity.Account.Services;

public class IdentityRoleService : IIdentityRoleService
{
    private readonly ProviderServices _providerServices;
    private readonly IIdentityService _identityService;
    //    //var entity = ProviderServices.Mapper.Map<RegisterRequest, UserEntity>(parameter);
    //    var entity = new UserEntity(parameter);

    //    await _identityService.RoleManager.CreateAsync(new RoleEntity { Name = "Admin" });

    //    var pass = _identityService.UserManager.PasswordHasher.HashPassword(entity, parameter.Password);
    //    await _identityService.UserManager.CreateAsync(entity, pass);

    //    await _identityService.UserManager.AddToRolesAsync(entity, parameter.Roles);
    public IdentityRoleService(ProviderServices providerServices, IIdentityService service)
    {
        _providerServices = providerServices;
        _identityService = service;
    }

    public async Task<IdentityResult> CreateAsync(RoleCreateRequest parameter) 
    {
        var entity = new RoleEntity(parameter.Name);

        var restult = await _identityService.RoleManager.CreateAsync(entity);

        return restult;
    }
}
