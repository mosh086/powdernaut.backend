using Microsoft.AspNetCore.Http;
using Powdernaut.Application.UseCases.Identity.Account.Services;

namespace Powdernaut.Application.UseCases.Identity.Authenticate.Handlers.User.Login;

public class UserLoginHandler : Handler<UserLoginRequest, UserLoginResponse>
{
    private readonly IIdentityUserService _service;
    private readonly ISignInService _signInService;
    public UserLoginHandler(ProviderServices providerServices, IIdentityUserService service, ISignInService signInService) : base(providerServices)
    {
        _service = service;
        _signInService = signInService;
    }

    public override async Task<UserLoginResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken) 
    {
        try
        {
            (var result, var token) = await _signInService.PasswordSignInAsync(request);
            return new UserLoginResponse(token?.Token ?? string.Empty);
        }
        catch (Exception ex)
        {
            throw new Exception();
        }
        
    }
}
