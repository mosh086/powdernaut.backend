using Powdernaut.Application.Common.Models.DTOs;

namespace Powdernaut.Infrastructure.Identity.Models;



public class IdentityOption
{
    public JwtOptions Jwt { get; set; } = default!;
}
public class JwtOptions
{
    public int ExpireMinutes { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
}



public class IdentityResult<T> : IdentityResult
{
    public T Result { get; set; }
    public IdentityResult(bool success, T value)
    {
        Succeeded = success;
        Result = value;
    }
}