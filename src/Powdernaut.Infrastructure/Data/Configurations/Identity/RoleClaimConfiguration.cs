using Powdernaut.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Powdernaut.Infrastructure.Data.Configurations.Identity;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaimEntity>
{
    public void Configure(EntityTypeBuilder<RoleClaimEntity> builder)
    {
        //builder.ToTable("RoleClaims", "Security");
        //builder.HasKey(item => item.Id);
        //builder.Property(item => item.RoleId).HasColumnName("RoleId");
        //builder.Property(item => item.ClaimType).HasColumnName("ClaimType");
        //builder.Property(item => item.ClaimValue).HasColumnName("ClaimValue");
        //builder.AddAuditableMapping<RoleClaimEntity, int>();
    }
}
