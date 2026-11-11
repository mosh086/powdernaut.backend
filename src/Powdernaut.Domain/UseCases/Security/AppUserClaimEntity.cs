namespace Powdernaut.Domain.UseCases.Security;

[Table("UserClaim", Schema = "sec"), Description("User Claim Entity Model")]
public class AppUserClaimEntity : BaseAuditableEntity
{
    public long UserId { get; private set; } = default!;
    public string? ClaimType { get; private set; }
    public string? ClaimValue { get; private set; }
}
