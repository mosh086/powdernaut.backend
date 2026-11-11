using Powdernaut.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authentication;

namespace Powdernaut.Infrastructure.Identity.Polymorphism;

public class AppSignInManager<TUser> : SignInManager<TUser> where TUser : UserEntity
{
    private readonly UserManager<TUser> _userManager;
    public AppSignInManager(
        UserManager<TUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<TUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<TUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<TUser> confirmation)
        : base(
            userManager,
            contextAccessor,
            claimsFactory,
            optionsAccessor,
            logger,
            schemes,
            confirmation)
    {
        _userManager = userManager;
    }
    public override async Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string? authenticationMethod = null)
    {
        await base.SignInAsync(user, authenticationProperties, authenticationMethod);
    }

    public override async Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }
}