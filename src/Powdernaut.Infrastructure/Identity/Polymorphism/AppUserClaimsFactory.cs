using Powdernaut.Infrastructure.Identity.Entities;

namespace Powdernaut.Infrastructure.Identity.Polymorphism;

public class AppUserClaimsFactory : UserClaimsPrincipalFactory<UserEntity, RoleEntity>
{
    public AppUserClaimsFactory(
        UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager,
        IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options)
    {
    }

    protected override Task<ClaimsIdentity> GenerateClaimsAsync(UserEntity user)
    {
        return base.GenerateClaimsAsync(user);
    }
}