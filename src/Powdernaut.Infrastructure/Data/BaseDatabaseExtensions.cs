using Powdernaut.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Builder;

namespace Powdernaut.Infrastructure.Data;

public static class BaseDatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
    }

    public static ModelBuilder AddSecurityConfiguration(this ModelBuilder builder)
    {
        builder.Entity<UserEntity>().ToTable("User", "sec");

        builder.Entity<UserClaimEntity>().ToTable("UserClaim", "sec");

        builder.Entity<UserLoginEntity>().ToTable("UserLogin", "sec");

        builder.Entity<UserRoleEntity>().ToTable("UserRole", "sec");

        builder.Entity<UserTokenEntity>().ToTable("UserToken", "sec");

        builder.Entity<RoleEntity>().ToTable("Role", "sec");

        builder.Entity<RoleClaimEntity>().ToTable("RoleClaim", "sec");
        return builder;
    }
}