using Powdernaut.Application.Common.Models.DTOs;
using Powdernaut.Application.Providers;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Models;
using Powdernaut.Infrastructure.Identity.Repositories;

namespace Powdernaut.Infrastructure.Identity.Service;

public class JwtTokenService : ITokenService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IdentityOption _identityOption;
    private readonly ILogger<JwtTokenService> _logger;
    private const string loginProvider = "JWT";
    private const string tokenName = "AccessToken";
    private string RefreshToken = "";
    private readonly ProviderServices _providerServices;
    public JwtTokenService(
        UserManager<UserEntity> userManager,
        IOptions<IdentityOption> options,
        ILogger<JwtTokenService> logger,
        ProviderServices providerServices)
    {
        _userManager = userManager;
        _identityOption = options.Value;
        _logger = logger;
        RefreshToken = GenerateRefreshToken();
        _providerServices = providerServices;
    }

    public async Task<AuthResponse> GenerateAccessTokenAsync(UserEntity user)
    {
        try
        {
            var claims = await GetUserClaimsAsync(user);
            var token = await GenerateJwtToken(user, claims);
            var refreshToken = GenerateRefreshToken();
            //await TrackLoginAsync();
            return new AuthResponse
            {
                Token = token,
                RefreshToken = RefreshToken,
                ExpiresIn = DateTime.UtcNow.AddMinutes(_identityOption.Jwt.ExpireMinutes),
                User = MapToUserProfileDto(user)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating access token for user {UserId}", user.Id);
            throw;
        }
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true, // We want to validate expired tokens
            ValidateIssuerSigningKey = true,
            ValidIssuer = _identityOption.Jwt.Issuer,
            ValidAudience = _identityOption.Jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityOption.Jwt.Key)),
            ClockSkew = TimeSpan.FromMinutes(1) // Added small clock skew
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private async Task<List<Claim>> GetUserClaimsAsync(UserEntity user)
    {

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new("Name", user.Name ?? string.Empty),
            new("PhoneNumber", user.PhoneNumber ?? string.Empty),
            new("UserId", user.Id.ToString()),
            new("UserRoleId", user.Id.ToString()),//    TODO
            new("RoleName", user.Id.ToString()), // TODO
            new("RoleTitle", user.Id.ToString()), //TODO
            new("Username", user.UserName ?? string.Empty),
            new("Email", user.Email ?? string.Empty),
            new("SecurityStamp", user.SecurityStamp ?? string.Empty)
        };

        // Add roles as individual claims
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }
        claims.ForEach(item =>
        {
            _providerServices.Convertor.Serialize(item.Value);
        });
        // Handle custom claims carefully
        var userClaims = await _userManager.GetClaimsAsync(user);
        userClaims.ToArray().ToList().AddRange(userClaims);
        await _userManager.RemoveClaimsAsync(user, userClaims);
        await _userManager.AddClaimsAsync(user, claims);

        return claims;
    }

    private async Task<string> GenerateJwtToken(UserEntity user, IEnumerable<Claim> claims)
    {
        // Validate claims first
        var validClaims = new List<Claim>();
        foreach (var claim in claims)
        {
            try
            {
                // Test if the value can be serialized
                var test = JsonSerializer.Serialize(claim.Value);
                validClaims.Add(claim);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid claim {claim.Type} with value {claim.Value}: {ex.Message}");
                // Convert complex claims to JSON strings
                validClaims.Add(new Claim(claim.Type, JsonSerializer.Serialize(claim.Value)));
            }
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityOption.Jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: _identityOption.Jwt.Issuer,
        audience: _identityOption.Jwt.Audience,
        claims: validClaims, // Use the validated claims
        expires: DateTime.UtcNow.AddMinutes(_identityOption.Jwt.ExpireMinutes),
        signingCredentials: creds
        );
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        await _userManager.SetAuthenticationTokenAsync(user, loginProvider, RefreshToken, tokenValue);
        return tokenValue;
    }

    private UserProfileDTO MapToUserProfileDto(UserEntity user)
    {
        return new UserProfileDTO
        {
            Id = user.Id,
            EntityId = user.EntityId.Value,
            Username = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            Name = user.Name ?? string.Empty
        };
    }
}