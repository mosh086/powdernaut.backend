using Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetAll;
using Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetById;

namespace Powdernaut.WebApi.Controllers.Security
{
    public class RoleController : AuthorizationController
    {
        [HttpGet]
        public async Task<IActionResult> Get() => await RequestAsync<RoleGetAllRequest, IEnumerable<RoleGetAllResponse>>(new RoleGetAllRequest());

        [HttpGet("{entityId}")]
        public async Task<IActionResult> Get(Guid entityId) => await RequestAsync<RoleGetByIdRequest, RoleGetByIdResponse>(new RoleGetByIdRequest(entityId));
    }
}
