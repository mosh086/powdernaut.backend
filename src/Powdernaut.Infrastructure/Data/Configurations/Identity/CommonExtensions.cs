using Powdernaut.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Powdernaut.Infrastructure.Data.Configurations.Identity;

public static class CommonExtensions
{
    public static void AddAuditableMapping<TEntity, TId>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAuditableEntity<TId>
        where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    {
        //builder.Property(item => item.EntityId).HasColumnName("EntityId");
        builder.Property(item => item.IsDeleted).HasColumnName("IsDeleted");
        builder.Property(item => item.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(item => item.CreatedBy).HasColumnName("CreatedBy").IsRequired();
        builder.Property(item => item.LastUpdatedAt).HasColumnName("UpdatedAt");
        builder.Property(item => item.LastUpdatedBy).HasColumnName("LastUpdatedBy");
    }
}
