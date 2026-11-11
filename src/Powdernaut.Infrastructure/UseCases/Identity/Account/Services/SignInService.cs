using Newtonsoft.Json.Linq;
using Powdernaut.Application.Common.Models.DTOs;
using Powdernaut.Application.Providers;
using Powdernaut.Application.UseCases.Identity.Account.Services;
using Powdernaut.Application.UseCases.Identity.Authenticate.Handlers.User.Login;
using Powdernaut.Domain.Common;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Models;
using Powdernaut.Infrastructure.Identity.Repositories;
using System.Data.Common;
using System.Reflection.Metadata;
using static Dapper.SqlMapper;

namespace Powdernaut.Infrastructure.UseCases.Identity.Account.Services;

public class SignInService : ISignInService
{
    private readonly ProviderServices _providerServices;
    private readonly IIdentityService _identityService;

    public SignInService(ProviderServices providerServices, IIdentityService service)
    {
        _providerServices = providerServices;
        _identityService = service;
    }

    public async Task<(SignInResult, AuthResponse?)> PasswordSignInAsync(UserLoginRequest request)
    {
        var token = new AuthResponse();
        
        var userEntity = await _identityService.UserManager.FindByEmailAsync(request.Username);

        var loginResult = await _identityService.SignInManager.PasswordSignInAsync(userEntity, request.Password, request.IsRemember, false);

        if (loginResult.Succeeded)
        {
            token = await _identityService.LoginAsync(userEntity);
        }

        return (loginResult, token);
    }
}
