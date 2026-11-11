using Powdernaut.Application.Providers.UserManagement;
using Powdernaut.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Powdernaut.Infrastructure.Data.Interceptors;

public static class AuditableShadowProperties
{
    public static readonly Func<object, bool> EFPropertyIsDeleted = entity => EF.Property<bool>(entity, IsDeleted!);
    public static readonly string IsDeleted = nameof(IsDeleted);

    public static readonly Func<object, long> EFPropertyCreatedBy = entity => EF.Property<long>(entity, CreatedBy!);
    public static readonly string CreatedBy = nameof(CreatedBy);

    public static readonly Func<object, long?> EFPropertyLastUpdatedBy = entity => EF.Property<long?>(entity, LastUpdatedBy!);
    public static readonly string LastUpdatedBy = nameof(LastUpdatedBy);

    public static readonly Func<object, DateTime> EFPropertyCreatedAt = entity => EF.Property<DateTime>(entity, CreatedAt!);
    public static readonly string CreatedAt = nameof(CreatedAt);

    public static readonly Func<object, DateTime?> EFPropertyLastUpdatedAt = entity => EF.Property<DateTime?>(entity, LastUpdatedAt!);
    public static readonly string LastUpdatedAt = nameof(LastUpdatedAt);

    public static readonly Func<object, EntityId> EFPropertyEntityId = entity => EF.Property<EntityId>(entity, EntityId!);
    public static readonly string EntityId = nameof(EntityId);


    public static ModelBuilder AddAuditableShadowProperties<TId>(this ModelBuilder modelBuilder)
        where TId : struct,
              IComparable,
              IComparable<TId>,
              IConvertible,
              IEquatable<TId>,
              IFormattable
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(c => typeof(IAuditableEntity<TId>).IsAssignableFrom(c.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                        .Property<bool>(IsDeleted).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<long>(CreatedBy).HasMaxLength(50);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<long?>(LastUpdatedBy).HasMaxLength(50);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>(CreatedAt);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(LastUpdatedAt);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<EntityId>(EntityId).IsRequired().ValueGeneratedOnAdd();
        }
        return modelBuilder;
    }

    public static void SetAuditableEntityPropertyValues(
        this ChangeTracker changeTracker,
        IUser user)
    {
        var userAgent = user.Agent;
        var userIp = user.Ip;
        var now = DateTime.Now;
        var userId = user.UserId;

        foreach (var entry in changeTracker.Entries()) 
        {
            if (entry.State == EntityState.Modified && typeof(IAuditableEntity<long>).IsAssignableFrom(entry.Metadata.ClrType)) 
            {
                entry.Property(LastUpdatedAt).CurrentValue = now;
                entry.Property(LastUpdatedBy).CurrentValue = userId;
            }

            if (entry.State == EntityState.Added && typeof(IAuditableEntity<long>).IsAssignableFrom(entry.Metadata.ClrType)) 
            {
                entry.Property(CreatedAt).CurrentValue = now;
                entry.Property(CreatedBy).CurrentValue = userId;
                entry.Property(IsDeleted).CurrentValue = false;
            }
        }
    }
}