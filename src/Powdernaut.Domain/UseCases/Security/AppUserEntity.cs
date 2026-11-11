using Powdernaut.Domain.UseCases.Security.Parameters;

namespace Powdernaut.Domain.UseCases.Security;

[Table("User", Schema = "sec"), Description("User Entity Model")]
public partial class AppUserEntity : BaseAuditableEntity

{
    public string Name { get; private set; } = string.Empty;
    public string Family { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public string PersonalCode { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string NormalizedUserName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string NormalizedEmail { get; private set; } = string.Empty;
    public bool EmailConfirmed { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public string SecurityStamp { get; private set; } = string.Empty;
    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    public string PhoneNumber { get; private set; } = string.Empty;
    public bool PhoneNumberConfirmed { get; private set; }
    public bool TwoFactorEnabled { get; private set; }
    public DateTimeOffset? LockoutEnd { get; private set; }
    public bool LockoutEnabled { get; private set; }
    public int AccessFailedCount { get; private set; }

    private List<AppUserRoleEntity>? _userRoleEntities;
    public virtual IReadOnlyCollection<AppUserRoleEntity> UserRoleEntities => _userRoleEntities!;

    public void SetId(long id) => Id = id;

    public AppUserEntity()
    {
        
    }
    public AppUserEntity(AppUserCreateParameters parameters)
    {
        Name = parameters.Name;
        UserName = parameters.UserName;
        NormalizedUserName = UserName.ToUpper();
        Email = parameters.Email;
        NormalizedEmail = Email.ToUpper();
        PhoneNumber = parameters.PhoneNumber;

    }

    public AppUserEntity(long id, Guid entityId, string name, string userName, string email, string phoneNumber)
    {
        Id = id;
        EntityId = entityId;
        Name = name;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public AppUserEntity(AppUserEntity entity)
    {
        Id = entity.Id;
        EntityId = entity.EntityId;
        Name = entity.Name;
        Email = entity.Email;
        NormalizedEmail = entity.NormalizedEmail;
        EmailConfirmed = entity.EmailConfirmed;
        UserName = entity.UserName;
        NormalizedUserName = entity.NormalizedUserName;
        PhoneNumber = entity.PhoneNumber;
        PhoneNumberConfirmed = entity.PhoneNumberConfirmed;
        PasswordHash = entity.PasswordHash;
        SecurityStamp = entity.SecurityStamp;
        TwoFactorEnabled = entity.TwoFactorEnabled;
        LockoutEnd = entity.LockoutEnd;
        LockoutEnabled = entity.LockoutEnabled;
        AccessFailedCount = entity.AccessFailedCount;
        ConcurrencyStamp = entity.ConcurrencyStamp;
    }

    public void SetPassword(string securityStamp, string passwordHash, string concurrencyStamp)
    {
        SecurityStamp = securityStamp;
        PasswordHash = passwordHash;
        ConcurrencyStamp = concurrencyStamp;
    }

    public void AddUserRole(AppUserRoleEntity entity)
    {
        if (_userRoleEntities is null)
        {
            _userRoleEntities = new();
        }
        _userRoleEntities.Add(entity);
    }
}