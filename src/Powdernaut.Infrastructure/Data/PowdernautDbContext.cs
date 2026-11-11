using Powdernaut.Domain.UseCases.Security;
using Powdernaut.Infrastructure.Data.Interceptors;
using Powdernaut.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Powdernaut.Infrastructure.Data;

public class PowdernautDbContext : BaseDatabaseContext
{
    public PowdernautDbContext(DbContextOptions<PowdernautDbContext> options) : base(options)
    {
    }

    //public virtual DbSet<AppUserEntity> Users => Set<AppUserEntity>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        //configurationBuilder.Properties<Title>().HaveConversion<TitleConversion>();
        //configurationBuilder.Properties<Description>().HaveConversion<DescriptionConversion>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddAuditableShadowProperties<long>();
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

}
