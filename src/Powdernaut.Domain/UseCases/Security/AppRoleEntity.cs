namespace Powdernaut.Domain.UseCases.Security;

[Table("Role", Schema = "sec"), Description("Roles Entity Model")]
public class AppRoleEntity : BaseAuditableEntity
{
    public string? Title { get; private set; }
    public string Name { get; private set; }
    public string? NormalizedName { get; private set; }
    public string? ConcurrencyStamp { get; private set; }

    private List<AppUserRoleEntity>? _userRoleEntities;

    public virtual IReadOnlyCollection<AppUserRoleEntity> UserRoleEntities => _userRoleEntities!;

    public AppRoleEntity(string name)
    {
        Name = name;
        NormalizedName = name.ToUpper();
    }

    public AppRoleEntity(long id, string name, string normalizedName, string concurrencyStamp, string title)
    {
        Id = id;
        Name = name;
        NormalizedName = normalizedName;
        ConcurrencyStamp = concurrencyStamp;
        Title = title;
        //IsDeleted = isDeleted;
    }

    public void SetId(long id) => Id = id;
}
