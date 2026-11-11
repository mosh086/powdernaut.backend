using Microsoft.AspNetCore.Authorization;

namespace Powdernaut.WebApi.Common.Controllers;

[Authorize]
public abstract class AuthorizationController : BaseController
{

}