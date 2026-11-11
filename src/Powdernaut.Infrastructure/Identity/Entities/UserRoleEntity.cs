using Powdernaut.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Powdernaut.Infrastructure.Identity.Entities;

public class UserRoleEntity : IdentityUserRole<long>
{
    public UserRoleEntity() { }
    public UserRoleEntity(long userId, long roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

}
