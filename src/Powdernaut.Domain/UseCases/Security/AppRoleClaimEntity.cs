namespace Powdernaut.Domain.UseCases.Security;

[Table("RoleClaim", Schema = "sec"), Description("Role Claim Entity Model")]
public class AppRoleClaimEntity : BaseEntity<long>, IBaseEntity<long>
{
    public long RoleId { get; private set; }
    public string? ClaimType { get; private set; }
    public string? ClaimValue { get; private set; }
}
