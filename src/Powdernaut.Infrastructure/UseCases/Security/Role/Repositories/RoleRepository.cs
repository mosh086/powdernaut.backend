using Powdernaut.Application.Common.Repository;
using Powdernaut.Application.UseCases.Security.Role.Repositories;
using Powdernaut.Domain.UseCases.Security;
using Powdernaut.Infrastructure.Common.Repository;
using Powdernaut.Infrastructure.Data;
using Powdernaut.Infrastructure.Identity.Entities;
using System.Security.Cryptography;

namespace Powdernaut.Infrastructure.UseCases.Security.Role.Repositories;

public class RoleRepository : Repository<AppRoleEntity, long>, IRoleRepository
{
    private readonly RoleManager<RoleEntity> _roleManager;
    public RoleRepository(PowdernautDbContext context, RoleManager<RoleEntity> roleManager) : base(context)
    {
        _roleManager = roleManager;
    }

    public async Task<AppRoleEntity?> FindByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role is null)
            return null;
        return role.AppRoleEntity();
    }

    public override async Task<AppRoleEntity> AddAsync(AppRoleEntity entity, CancellationToken cancellationToken)
    {
        RoleEntity roleEntity = new RoleEntity(entity.Name);
        await _roleManager.CreateAsync(roleEntity);
        entity.SetId(roleEntity.Id);
        return entity;
    }

    public async Task<IDictionary<string, AppRoleEntity>> FindByNamesAsync(string[] roleNames, CancellationToken cancellationToken)
    {
        var roles = new Dictionary<string, AppRoleEntity>();
        foreach (var roleName in roleNames)
        {
            var entity = await _roleManager.FindByNameAsync(roleName);
            if (entity is null) continue;
            roles.Add(roleName, entity.AppRoleEntity());
        }
        return roles;
    }

    public override async Task<IEnumerable<AppRoleEntity>> GetAsync(CancellationToken cancellationToken)
        => await Context.Roles.AsNoTracking().Select(r => r.AppRoleEntity()).ToListAsync(cancellationToken);

    public override AppRoleEntity? GetAsNoTracking(long id, CancellationToken cancellationToken)
    {
        var entity = Context.Roles.AsNoTracking().FirstOrDefault(e => e.Id.Equals(id));
        if (entity is null) return null;
        return entity.AppRoleEntity();
    }


    public override AppRoleEntity? GetAsNoTracking(Guid entityId, CancellationToken cancellationToken)
        => Context.Roles.AsNoTracking().FirstOrDefault(item => item.EntityId.Equals(entityId))?.AppRoleEntity();

    public override async Task<AppRoleEntity?> GetAsNoTrackingAsync(long id, CancellationToken cancellationToken)
        => (await Context.Roles.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken))?.AppRoleEntity();

    public override async Task<AppRoleEntity?> GetAsNoTrackingAsync(Guid entityId, CancellationToken cancellationToken)
        => (await Context.Roles.AsNoTracking().FirstOrDefaultAsync(item => item.EntityId.Equals(entityId), cancellationToken))?.AppRoleEntity();
}