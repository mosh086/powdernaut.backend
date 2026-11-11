using Powdernaut.Application.UseCases.Security.Role.Repositories;
using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetAll;

public class RoleGetAllHandler : Handler<RoleGetAllRequest, IEnumerable<RoleGetAllResponse>>
{
    private readonly IRoleRepository _repository;
    public RoleGetAllHandler(ProviderServices providerServices, IRoleRepository repository) : base(providerServices)
    {
        _repository = repository;
    }

    public override async Task<IEnumerable<RoleGetAllResponse>> Handle(RoleGetAllRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetAsync(cancellationToken);
            var roles = ProviderServices.Mapper.Map<IEnumerable<AppRoleEntity>, IEnumerable<RoleGetAllResponse>>(data);
            return new List<RoleGetAllResponse>(); //Result<IEnumerable<RoleGetAllResponse>>.Ok(roles);
        }
        catch (Exception ex)
        {
            return new List<RoleGetAllResponse>(); //Result<IEnumerable<RoleGetAllResponse>>.Fail(500, ex.Message);
        }
    }
}
