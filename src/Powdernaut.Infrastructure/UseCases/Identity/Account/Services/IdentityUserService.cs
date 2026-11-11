using Microsoft.AspNetCore.Identity.Data;
using Powdernaut.Application.Providers;
using Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;
using Powdernaut.Application.UseCases.Identity.Account.Services;
using Powdernaut.Application.UseCases.Identity.Authenticate.Handlers.User.Login;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Repositories;

namespace Powdernaut.Infrastructure.UseCases.Identity.Account.Services;

public class IdentityUserService : IIdentityUserService
{
    private readonly ProviderServices _providerServices;
    private readonly IIdentityService _identityService;
    //    //var entity = ProviderServices.Mapper.Map<RegisterRequest, UserEntity>(parameter);
    //    var entity = new UserEntity(parameter);

    //    await _identityService.RoleManager.CreateAsync(new RoleEntity { Name = "Admin" });

    //    var pass = _identityService.UserManager.PasswordHasher.HashPassword(entity, parameter.Password);
    //    await _identityService.UserManager.CreateAsync(entity, pass);

    //    await _identityService.UserManager.AddToRolesAsync(entity, parameter.Roles);
    public IdentityUserService(ProviderServices providerServices, IIdentityService service)
    {
        _providerServices = providerServices;
        _identityService = service;
    }

    public async Task<IdentityResult> Register(UserRegisterRequest parameter) 
    {
        //var entity = _providerServices.Mapper.Map<UserRegisterRequest, UserEntity>(parameter);
        var entity = new UserEntity(parameter);

        //var pass = _identityService.UserManager.PasswordHasher.HashPassword(entity, parameter.Password);

        var restult = await _identityService.UserManager.CreateAsync(entity, parameter.Password);

        await _identityService.UserManager.AddToRolesAsync(entity, parameter.Roles);

        return restult;
    }

    //public async Task<IdentityResult> Login(UserLoginRequest parameter)
    //{
    //    //var entity = _providerServices.Mapper.Map<UserRegisterRequest, UserEntity>(parameter);
    //    //var entity = new UserEntity(parameter);

    //    var entity = _identityService.UserManager.FindByEmailAsync(parameter.Username);

    //    _identityService.UserManager.PasswordHasher.VerifyHashedPassword(entity.Result, entity.Result.PasswordHash, parameter.Password);

    //    return restult;
    //}
}
