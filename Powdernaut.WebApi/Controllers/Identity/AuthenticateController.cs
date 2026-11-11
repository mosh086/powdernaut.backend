using Powdernaut.Application.UseCases.Identity.Authenticate.Handlers.User.Login;

namespace Powdernaut.WebApi.Controllers.Identity;

public class AuthenticateController : BaseController
{
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginRequest request) => await RequestAsync<UserLoginRequest, UserLoginResponse>(request);

    //[HttpGet("LoginAs/{id}")]
    //public async Task<IActionResult> LoginAs(long id)
    //{
    //    var userEntity = await _identityService.UserManager.FindByIdAsync($"{id}");
    //    if (userEntity is null)
    //    {
    //        return NotFound();
    //    }
    //    var token = await _identityService.LoginAsync(userEntity);
    //    return Ok(token);
    //}

    //[HttpGet("Logout")]
    //public async Task<IActionResult> Logout()
    //{
    //    await _identityService.LogoutAsync();
    //    return Ok();
    //}

    //[HttpGet("IsAuthenticated")]
    //public async Task<IActionResult> IsAuthenticated()
    //{
    //    await Task.CompletedTask;
    //    return Ok(User?.Identity?.IsAuthenticated);
    //}

    //[Authorize]
    //[HttpGet("IsAuthorize")]
    //public async Task<IActionResult> Authorize()
    //{
    //    await Task.CompletedTask;
    //    return Ok(User?.Identity?.IsAuthenticated);
    //}

    //[HttpGet("CurrentUser")]
    //public IActionResult CurrentUser()
    //{
    //    return Ok(ProviderServices.User);
    //}

    //[HttpGet("validate-token")]
    //public IActionResult ValidateToken([FromHeader(Name = "Authorization")] string authHeader)
    //{
    //    if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
    //    {
    //        return BadRequest("Invalid Authorization header");
    //    }

    //    var token = authHeader.Substring("Bearer ".Length).Trim();

    //    try
    //    {
    //        var handler = new JwtSecurityTokenHandler();
    //        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

    //        return Ok(new
    //        {
    //            isValid = true,
    //            claims = jsonToken!.Claims.Select(c => new { c.Type, c.Value })
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new
    //        {
    //            isValid = false,
    //            error = ex.Message
    //        });
    //    }
    //}
}
