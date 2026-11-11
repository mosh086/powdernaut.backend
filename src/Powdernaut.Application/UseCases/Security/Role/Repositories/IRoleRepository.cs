using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Application.UseCases.Security.Role.Repositories;

public interface IRoleRepository : IRepository<AppRoleEntity, long>
{
    Task<AppRoleEntity?> FindByNameAsync(string roleName, CancellationToken cancellationToken);
    Task<IDictionary<string, AppRoleEntity>> FindByNamesAsync(string[] roleNames, CancellationToken cancellationToken);
}