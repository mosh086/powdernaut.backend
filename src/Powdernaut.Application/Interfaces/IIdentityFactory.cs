using Powdernaut.Application.Providers.Scrutor;
using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Application.Interfaces;

public interface IIdentityFactory : IScopeLifeTime
{
    string PasswordHash(AppUserEntity entity, string password);
    bool VerifyHashedPassword(AppUserEntity entity, string passwordHash, string password);
    string ConcurrencyStamp(string password);
    string SecurityStamp(string password);
}
