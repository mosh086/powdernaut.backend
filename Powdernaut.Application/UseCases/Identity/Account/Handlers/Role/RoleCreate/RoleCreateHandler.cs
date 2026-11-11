using Powdernaut.Application.UseCases.Identity.Account.Services;

namespace Powdernaut.Application.UseCases.Identity.Account.Handlers.Role.RoleCreate;

public class RoleCreateHandler : Handler<RoleCreateRequest, RoleCreateResponse>
{
    private readonly IIdentityRoleService _service;
    public RoleCreateHandler(ProviderServices providerServices, IIdentityRoleService service) : base(providerServices)
    {
        _service = service;
    }
    public override async Task<RoleCreateResponse> Handle(RoleCreateRequest request, CancellationToken cancellationToken) 
    {
        var result = await _service.CreateAsync(request);
        return new RoleCreateResponse(); //Result<RoleCreateResponse>.Ok(new RoleCreateResponse()); //new RoleCreateResponse($"Create Success Role : { result.Succeeded }");
    }
}
