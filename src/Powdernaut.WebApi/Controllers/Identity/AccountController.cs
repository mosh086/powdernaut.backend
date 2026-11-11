using Powdernaut.Application.UseCases.Identity.Account.Handlers.Role.RoleCreate;
using Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;

namespace Powdernaut.WebApi.Controllers.Identity;

public class AccountController : AuthorizationController
{
    [AllowAnonymous]
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(UserRegisterRequest request) => await RequestAsync<UserRegisterRequest, UserRegisterResponse>(request);

    [HttpPost]
    [Route("Role")]
    public async Task<IActionResult> Role(RoleCreateRequest request) => await RequestAsync<RoleCreateRequest, RoleCreateResponse>(request);

    //[AllowAnonymous]
    //[HttpDelete("Remove")]
    //public async Task<IActionResult> Remove()
    //{
    //    await Task.CompletedTask;
    //    return Ok(true);
    //}

    //[HttpPut("DisActive")]
    //public async Task<IActionResult> DisActive()
    //{
    //    await Task.CompletedTask;
    //    return Ok(true);
    //}

    //[HttpGet("Profile")]
    //public async Task<IActionResult> Profile()
    //{
    //    await Task.CompletedTask;
    //    return Ok(true);
    //}

    //[HttpGet("admin-and-user")]
    //[Authorize(Policy = Policies.AdminAccess)] // Multiple policies would require custom handler
    //public async Task<IActionResult> AdminAccess()
    //{
    //    await Task.CompletedTask;
    //    return Ok(true);
    //}

    //[HttpGet("UserAccess")]
    //[Authorize(Policy = Policies.UserAccess)]
    //public async Task<IActionResult> UserAccess()
    //{
    //    await Task.CompletedTask;
    //    return Ok(true);
    //}

    //[HttpGet("CanUserAccessAdminFeatures")]
    //public async Task<bool> CanUserAccessAdminFeatures()
    //{
    //    var result = await _identityService.AuthorizationService.AuthorizeAsync(
    //        _identityService.HttpContextAccessor.HttpContext!.User,
    //        Policies.AdminAccess);

    //    return result.Succeeded;
    //}
}
