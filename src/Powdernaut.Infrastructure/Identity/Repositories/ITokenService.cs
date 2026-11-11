using Powdernaut.Application.Common.Models.DTOs;
using Powdernaut.Application.Providers.Scrutor;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Models;

namespace Powdernaut.Infrastructure.Identity.Repositories;

public interface ITokenService : IScopeLifeTime
{
    Task<AuthResponse> GenerateAccessTokenAsync(UserEntity user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}