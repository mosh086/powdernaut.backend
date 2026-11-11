using Powdernaut.Domain.Common;

namespace Powdernaut.Infrastructure.Identity.Entities;

public class UserLoginEntity : IdentityUserLogin<long>
{
    // public long Id { get; private set; }

    public UserLoginEntity()
    {
    }
}