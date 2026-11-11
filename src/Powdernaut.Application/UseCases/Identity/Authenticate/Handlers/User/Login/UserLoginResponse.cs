namespace Powdernaut.Application.UseCases.Identity.Authenticate.Handlers.User.Login;

public class UserLoginResponse
{
    public string Token { get; set; } = string.Empty;

    public UserLoginResponse(string token)
    {
        Token = token;
    }
}