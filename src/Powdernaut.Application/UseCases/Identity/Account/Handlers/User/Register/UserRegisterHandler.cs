using Powdernaut.Application.UseCases.Identity.Account.Services;
using System.Net.Http.Headers;

namespace Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;

public class UserRegisterHandler : Handler<UserRegisterRequest, UserRegisterResponse>
{
    private readonly IIdentityUserService _service;
    //private readonly IUserRepository _userRepository;
    //private readonly IRoleRepository _roleRepository;
    //private readonly IUserRoleRepository _userRoleRepository;
    //public UserRegisterHandler(ProviderServices providerServices, IIdentityUserService service, IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository) : base(providerServices)
    public UserRegisterHandler(ProviderServices providerServices, IIdentityUserService service) : base(providerServices)
    {
        _service = service;
        //_userRepository = userRepository;
        //_roleRepository = roleRepository;
        //_userRoleRepository = userRoleRepository;
    }

    public override async Task<UserRegisterResponse> Handle(UserRegisterRequest request, CancellationToken cancellationToken) 
    {
        var restult = await _service.Register(request);
        //await _userRepository.BeginTransactionAsync();
        //// TODO  Check duplicate email, username, phonenumber

        //var roles = await _roleRepository.FindByNamesAsync(request.Roles, cancellationToken);

        //var entity = ProviderServices.Mapper.Map<UserRegisterRequest, AppUserEntity>(request);
        //_userRepository.SetPassword(request.Password);

        //entity = await _userRepository.AddAsync(entity, cancellationToken);

        //foreach (var role in roles)
        //{
        //    var userRoleEntity = new AppUserRoleEntity(entity.Id, role.Value.Id);
        //    await _userRoleRepository.AddAsync(userRoleEntity, cancellationToken);
        //}

        //await _userRepository.SaveChangeAsync();
        //await _userRepository.CommitTransactionAsync();
        return new UserRegisterResponse(""); //Result<UserRegisterResponse>.Ok(new UserRegisterResponse($"Create Success User : { restult.Succeeded }"));
        //return new UserRegisterResponse($"Create Success User : ");
    }
}
