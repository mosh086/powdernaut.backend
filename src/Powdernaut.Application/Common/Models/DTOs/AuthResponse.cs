namespace Powdernaut.Application.Common.Models.DTOs;

public class AuthResponse
{
    public string RefreshToken { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public UserProfileDTO User { get; set; } = default!;
    public DateTime ExpiresIn { get; set; }
}
