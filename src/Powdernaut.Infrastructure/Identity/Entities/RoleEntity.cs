using Powdernaut.Domain.Common;
using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Infrastructure.Identity.Entities;

public class RoleEntity : IdentityRole<long>, IAuditableEntity<long>
{
    public EntityId EntityId { get; private set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }

    public DateTime? LastUpdatedAt { get; private set; }
    public long? LastUpdatedBy { get; private set; }

    public DateTime? DeletedAt { get; private set; }
    public long? DeletedBy { get; private set; }

    public bool IsDeleted { get; private set; }

    public void Access()
    {
        IsDeleted = false;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    public string? Title { get; private set; }

    public RoleEntity()
    {
        IsDeleted = false;
    }
    public RoleEntity(string name)
    {
        Name = name;
        Title = name;
    }

    public AppRoleEntity AppRoleEntity()
    {
        return new AppRoleEntity(
            Id,
            Name ?? string.Empty,
            NormalizedName ?? string.Empty,
            ConcurrencyStamp ?? string.Empty,
            Title ?? string.Empty);
    }
}
