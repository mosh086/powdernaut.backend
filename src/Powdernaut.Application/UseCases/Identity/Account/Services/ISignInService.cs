using Microsoft.AspNetCore.Identity;
using Powdernaut.Application.UseCases.Identity.Authenticate.Handlers.User.Login;

namespace Powdernaut.Application.UseCases.Identity.Account.Services;

public interface ISignInService
{
    Task<(SignInResult, AuthResponse?)> PasswordSignInAsync(UserLoginRequest request);
}
