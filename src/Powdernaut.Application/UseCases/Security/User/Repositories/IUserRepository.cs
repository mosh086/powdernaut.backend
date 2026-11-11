using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Application.UseCases.Security.User.Repositories;

public interface IUserRepository : IRepository<AppUserEntity, long>
{
    Task<AppUserEntity?> GetByEmailAsync(string email);
    Task<AppUserEntity?> GetByUsernameAsync(string username);
    void SetPassword(string password);
}
