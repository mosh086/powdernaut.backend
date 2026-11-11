using Powdernaut.Application.Providers;
using Powdernaut.Application.UseCases.Security.Role.Repositories;
using Powdernaut.Application.UseCases.Security.User.Repositories;
using Powdernaut.Domain.UseCases.Security;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Models;
using Powdernaut.Infrastructure.Identity.Parameters;
using Powdernaut.Infrastructure.Identity.Repositories;
using Microsoft.AspNetCore.Authorization;
using Powdernaut.Application.Common.Models.DTOs;

namespace Powdernaut.Infrastructure.Identity.Service;

public class IdentityService : IIdentityService
{
    private readonly ILogger<IdentityService> _logger;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IUserClaimsPrincipalFactory<UserEntity> _userClaimsPrincipalFactory;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ProviderServices _providerServices;
    public IdentityService(
        ILogger<IdentityService> logger,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager,
        RoleManager<RoleEntity> roleManager,
        IUserClaimsPrincipalFactory<UserEntity> userClaimsPrincipalFactory,
        ITokenService tokenService,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor,
        ProviderServices providerServices
        )
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _providerServices = providerServices;
    }

    public UserManager<UserEntity> UserManager => _userManager;
    public SignInManager<UserEntity> SignInManager => _signInManager;
    public RoleManager<RoleEntity> RoleManager => _roleManager;
    public IUserRepository UserRepository => _userRepository;
    public IRoleRepository RoleRepository => _roleRepository;
    public ITokenService TokenService => _tokenService;

    public IAuthorizationService AuthorizationService => _authorizationService;

    public IHttpContextAccessor HttpContextAccessor => _httpContextAccessor;

    public async Task<AuthResponse> LoginAsync(UserEntity entity)
    {
        var token = await TokenService.GenerateAccessTokenAsync(entity);

        await _signInManager.SignInAsync(entity, true);

        return token;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<AppUserEntity, UserCreateParameters>()
            .ReverseMap()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));
    }
}