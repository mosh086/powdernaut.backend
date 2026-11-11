namespace Powdernaut.Application.UseCases.Identity.Authenticate.Handlers.User.Login;

public class UserLoginRequest : RequestModel<UserLoginResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsRemember { get; set; }
}
