using Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;
using Powdernaut.Application.Utilities.Extensions;
using Powdernaut.Domain.Common;
using Powdernaut.Domain.UseCases.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powdernaut.Infrastructure.Identity.Entities;

[Table("User", Schema = "sec")]
public class UserEntity : IdentityUser<long>, IAuditableEntity<long>
{
    public EntityId EntityId { get; private set; } = Guid.NewGuid();
    public string? Name { get; private set; }

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
    public void DisActive()
    {
        IsDeleted = false;
    }
    private UserEntity()
    {

    }

    public UserEntity(UserRegisterRequest entity)
    {
        Name = entity.Name;
        Email = entity.Email;
        NormalizedEmail = entity.Email.ToUpper();
        UserName = entity.Email.ToUserName();
        NormalizedUserName = entity.Email.ToUserName().ToUpper();
        PhoneNumber = entity.PhoneNumber;
        
    }
    public UserEntity(AppUserEntity entity)
    {
        Name = entity.Name;
        Email = entity.Email;
        UserName = entity.UserName;
        PhoneNumber = entity.PhoneNumber;
    }

    public UserEntity(UserEntity entity, AppUserEntity parameters)
    {
        Name = parameters.Name;
        Email = parameters.Email;
        UserName = parameters.UserName;
        PhoneNumber = parameters.PhoneNumber;

        Id = entity.Id;
        EntityId = entity.EntityId;
        EmailConfirmed = entity.EmailConfirmed;
        PhoneNumberConfirmed = entity.PhoneNumberConfirmed;
        PasswordHash = entity.PasswordHash;
        SecurityStamp = entity.SecurityStamp;
        TwoFactorEnabled = entity.TwoFactorEnabled;
        LockoutEnd = entity.LockoutEnd;
        LockoutEnabled = entity.LockoutEnabled;
        AccessFailedCount = entity.AccessFailedCount;
        ConcurrencyStamp = entity.ConcurrencyStamp;
        CreatedAt = entity.CreatedAt;
        CreatedBy = entity.CreatedBy;
    }

    public AppUserEntity AppUserEntity()
    {
        return new AppUserEntity(
            Id,
            EntityId.Value,
            Name ?? string.Empty,
            UserName ?? string.Empty,
            Email ?? string.Empty,
            PhoneNumber ?? string.Empty);
    }
}
