using Powdernaut.Application.UseCases.Security.Role.Repositories;
using Powdernaut.Domain.UseCases.Security;
using System.Net.Http.Headers;

namespace Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetById;

public class RoleGetByIdHandler : Handler<RoleGetByIdRequest, RoleGetByIdResponse>
{
    private readonly IRoleRepository _repository;
    public RoleGetByIdHandler(ProviderServices providerServices, IRoleRepository repository) : base(providerServices)
    {
        _repository = repository;
    }

    public override async Task<RoleGetByIdResponse> Handle(RoleGetByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetAsNoTrackingAsync(request.EntityId, cancellationToken);
            if (data is null)
            {
                return new RoleGetByIdResponse(); // Result<RoleGetByIdResponse>.Ok(new RoleGetByIdResponse());
            }
            var result = ProviderServices.Mapper.Map<AppRoleEntity, RoleGetByIdResponse>(data);
            return new RoleGetByIdResponse(); // Result<RoleGetByIdResponse>.Ok(result);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}
