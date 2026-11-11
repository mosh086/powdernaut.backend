namespace Powdernaut.Domain.UseCases.Security;

[Table("UserRole", Schema = "sec"), Description("User Role Entity Model")]
public class AppUserRoleEntity
{
    [ForeignKey(nameof(AppUserEntity))]
    public virtual long UserId { get; set; }
    public virtual AppUserEntity AppUserEntity { get; set; } = new();

    [ForeignKey(nameof(AppRoleEntity))]
    public virtual long RoleId { get; set; }
    public virtual AppRoleEntity AppRoleEntity { get; set; } = default!;

    public AppUserRoleEntity(long userId, long roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}
