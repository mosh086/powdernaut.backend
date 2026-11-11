using Powdernaut.Application.UseCases.Security.Role.Repositories;
using Powdernaut.Application.UseCases.Security.User.Repositories;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Powdernaut.Application.Common.Models.DTOs;

namespace Powdernaut.Infrastructure.Identity.Repositories;

public interface IIdentityService
{
    public UserManager<UserEntity> UserManager { get; }
    public RoleManager<RoleEntity> RoleManager { get; }
    public SignInManager<UserEntity> SignInManager { get; }

    public IAuthorizationService AuthorizationService { get; }
    public IHttpContextAccessor HttpContextAccessor { get; }

    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public ITokenService TokenService { get; }

    Task<AuthResponse> LoginAsync(UserEntity entity);
    Task LogoutAsync();
}
