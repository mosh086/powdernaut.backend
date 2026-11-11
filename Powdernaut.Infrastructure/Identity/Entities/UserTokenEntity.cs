using Powdernaut.Infrastructure.Identity.Entities.Parameters;

namespace Powdernaut.Infrastructure.Identity.Entities;

public class UserTokenEntity : IdentityUserToken<long>
{
    public UserTokenEntity() { }
    public string RefreshToken { get; private set; }
    public void SetToken(string refToken) => RefreshToken = refToken;

    public UserTokenEntity(UserTokenParameters parameters)
    {
        UserId = parameters.UserId;
        LoginProvider = parameters.LoginProvider;
        Name = parameters.Name;
        Value = parameters.Value;
        RefreshToken = parameters.RefreshToken;
    }
}
