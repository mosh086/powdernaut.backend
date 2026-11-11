using Powdernaut.Domain.Common;

namespace Powdernaut.Infrastructure.Identity.Entities;

public class UserClaimEntity : IdentityUserClaim<long>
{
    public long Id { get; private set; }

    public UserClaimEntity()
    {
    }
}
