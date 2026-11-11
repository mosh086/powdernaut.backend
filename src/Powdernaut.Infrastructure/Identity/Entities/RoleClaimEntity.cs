namespace Powdernaut.Infrastructure.Identity.Entities;

public class RoleClaimEntity : IdentityRoleClaim<long>
{
    public long Id { get; set; }

    public RoleClaimEntity()
    {
    }
}
