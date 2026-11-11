namespace Powdernaut.Domain.UseCases.Security;

[Table("UserToken", Schema = "sec"), Description("User Token Entity Model")]
public class AppUserTokenEntity
{
    public long UserId { get; set; } = default!;
    public string LoginProvider { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Value { get; set; }
    public string? RefreshToken { get; set; }
}
