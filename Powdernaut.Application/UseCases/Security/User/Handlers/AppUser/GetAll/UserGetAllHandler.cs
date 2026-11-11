using Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetAll;
using Powdernaut.Application.UseCases.Security.User.Repositories;
using Powdernaut.Domain.UseCases.Security;

namespace Powdernaut.Application.UseCases.Security.User.Handlers.AppUser.GetAll;

public class UserGetAllHandler : Handler<UserGetAllRequest, IEnumerable<UserGetAllResponse>>
{
    private readonly IUserRepository _repository;
    public UserGetAllHandler(ProviderServices providerServices, IUserRepository repository) : base(providerServices)
    {
        _repository = repository;
    }

    public override async Task<IEnumerable<UserGetAllResponse>> Handle(UserGetAllRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetAsync(cancellationToken);
            var users = ProviderServices.Mapper.Map<IEnumerable<AppUserEntity>, IEnumerable<UserGetAllResponse>>(data);
            return new List<UserGetAllResponse>(); //Result<IEnumerable<UserGetAllResponse>>.Ok(users);
        }
        catch (Exception ex)
        {
            return new List<UserGetAllResponse>(); //Result<IEnumerable<UserGetAllResponse>>.Fail(500, ex.Message);
        }
    }
}