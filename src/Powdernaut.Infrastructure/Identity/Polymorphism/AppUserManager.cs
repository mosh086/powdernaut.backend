using Powdernaut.Infrastructure.Data;
using Powdernaut.Infrastructure.Identity.Entities;
using Powdernaut.Infrastructure.Identity.Entities.Parameters;

//using Powdernaut.Infrastructure.Identity.Entities.Parameters;
using Powdernaut.Infrastructure.Identity.Models;
using Newtonsoft.Json;

namespace Powdernaut.Infrastructure.Identity.Polymorphism;

public class AppUserManager<TUser> : UserManager<TUser> where TUser : UserEntity
{
    private readonly PowdernautDbContext Context;
    private const string LoginProvider = "JWT";
    private const string TokenName = "AccessToken";
    public AppUserManager(
        IUserStore<TUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<TUser> passwordHasher,
        IEnumerable<IUserValidator<TUser>> userValidators,
        IEnumerable<IPasswordValidator<TUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<TUser>> logger,
        PowdernautDbContext context)
        : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
    {
        Context = context;
    }
    public override Task<IList<string>> GetRolesAsync(TUser user)
    {
        return base.GetRolesAsync(user);
    }
    /// <summary>
    /// tokenName is RefreshToken
    /// </summary>
    /// <param name="user"></param>
    /// <param name="loginProvider"></param>
    /// <param name="tokenName"></param>
    /// <param name="tokenValue"></param>
    /// <returns></returns>
    public override async Task<IdentityResult> SetAuthenticationTokenAsync(TUser user, string loginProvider, string tokenName, string? tokenValue)
    {
        var parameters = new UserTokenParameters(user.Id, LoginProvider, TokenName, tokenValue ?? string.Empty, tokenName);
        if (Context.Set<UserTokenEntity>().Any(item => item.UserId == user.Id && item.LoginProvider == loginProvider))
        {
            return new IdentityResult<string>(true, JsonConvert.SerializeObject(parameters));
        }
        var userToken = new UserTokenEntity(parameters);
        await Context.Set<UserTokenEntity>().AddAsync(userToken);
        await Context.SaveChangesAsync();
        await AddLoginAsync(user, new UserLoginInfo(LoginProvider, TokenName, user.UserName));
        return new IdentityResult<string>(true, JsonConvert.SerializeObject(parameters));
    }
}