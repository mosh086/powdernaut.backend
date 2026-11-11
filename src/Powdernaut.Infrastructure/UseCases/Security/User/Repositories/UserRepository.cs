using Powdernaut.Application.Providers.ObjectMapper;
using Powdernaut.Application.UseCases.Security.User.Repositories;
using Powdernaut.Domain.Common;
using Powdernaut.Domain.UseCases.Security;
using Powdernaut.Infrastructure.Common.Repository;
using Powdernaut.Infrastructure.Data;
using Powdernaut.Infrastructure.Exceptions;
using Powdernaut.Infrastructure.Identity.Entities;

namespace Powdernaut.Infrastructure.UseCases.Security.User.Repositories;

public class UserRepository : Repository<AppUserEntity, long>, IUserRepository
{
    private readonly UserManager<UserEntity> _userManager;
    public string Password { get; private set; }
    private readonly IObjectMapper _mapper;
    public UserRepository(PowdernautDbContext context, UserManager<UserEntity> userManager, IObjectMapper mapper) : base(context)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public override async Task<AppUserEntity> AddAsync(AppUserEntity entity, CancellationToken cancellationToken)
    {
        try
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            UserEntity userEntity = new UserEntity(entity);

            var result = await _userManager.CreateAsync(userEntity, Password);
            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }
            entity.SetId(userEntity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public override async Task<AppUserEntity> UpdateAsync(AppUserEntity entity, CancellationToken cancellationToken)
    {
        try
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var user = await Context.Users.AsNoTracking().FirstAsync(u => u.EntityId == entity.EntityId);

            UserEntity userEntity = new UserEntity(user, entity);

            Context.ChangeTracker.Clear();
            var result = await _userManager.UpdateAsync(userEntity);
            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }
            entity.SetId(userEntity.Id);

            return entity;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public override void UpdateRange(IEnumerable<AppUserEntity> entities, CancellationToken cancellationToken)
    {
        var users = entities.Select(entity => 
        {
            return _userManager.Users.SingleOrDefault(item => item.EntityId.Equals(entity.EntityId.Value)).AppUserEntity();
        });

        Entity.UpdateRange(users);
    }

    public override IEnumerable<AppUserEntity> Get(CancellationToken cancellationToken)
        => _mapper.Map<UserEntity, AppUserEntity>(_userManager.Users);

    public override async Task<AppUserEntity?> GetAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _userManager.Users.SingleOrDefaultAsync(item => item.Id == id);
        if (entity is null) return (AppUserEntity?)null;
        return _mapper.Map<UserEntity, AppUserEntity>(entity);
    }

    public override AppUserEntity? Get(long id, CancellationToken cancellationToken)
    {
        var entity = _userManager.Users.SingleOrDefault(item => item.Id == id);
        if (entity is null) return (AppUserEntity?)null;
        return _mapper.Map<UserEntity, AppUserEntity>(entity);
    }

    public override async Task<AppUserEntity?> GetAsync(Guid entityId, CancellationToken cancellationToken)
        => (await _userManager.Users.SingleOrDefaultAsync(item => item.EntityId.Equals(entityId)))?.AppUserEntity();

    public override AppUserEntity? Get(Guid entityId, CancellationToken cancellationToken)
    { 
        var entity = _userManager.Users.SingleOrDefault(item => item.EntityId.Equals(entityId));
        if (entity is null) return (AppUserEntity?)null;
        return _mapper.Map<UserEntity, AppUserEntity>(entity);
    }
    
    public override async Task<IEnumerable<AppUserEntity>> GetAsync(CancellationToken cancellationToken)
        => await Context.Users
            .AsNoTracking()
            .Where(u => u.IsDeleted == false)
            .Select(u => u.AppUserEntity())
            .ToListAsync(cancellationToken);

    public void SetPassword(string password)
    {
        Password = password;
    }

    public async Task<AppUserEntity?> GetByEmailAsync(string email)
    {
        var test1 = await Entity.Where(item => item.Email.ToLower().Equals(email.ToLower())).SingleOrDefaultAsync()!;
        var test2 = await _userManager.FindByEmailAsync(email);

        return test1;
    }

    public async Task<AppUserEntity?> GetByUsernameAsync(string username)
    {
        var test1 = await _userManager.FindByNameAsync(username);
        var test2 = await Entity.Where(item => item.UserName == username).SingleOrDefaultAsync()!;

        return test2;
    }


    public override bool Remove(AppUserEntity entity, CancellationToken cancellationToken)
    {
        var item = Context.UserEntities.Single(item => item.Id.Equals(entity.Id));
        item.Delete();
        Context.SaveChanges();
        return true;
    }
    public override bool Remove(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = Context.UserEntities.Single(item => item.EntityId.Equals(entityId));
        entity.Delete();
        Context.SaveChanges();
        return true;
    }
    public override bool Remove(long id, CancellationToken cancellationToken)
    {
        var entity = Context.UserEntities.Single(item => item.Id == id);
        entity.Delete();
        Context.SaveChanges();
        return true;
    }
    public override async Task<bool> RemoveAsync(AppUserEntity entity, CancellationToken cancellationToken)
    {
        var item = await Context.UserEntities.SingleAsync(item => item.Id.Equals(entity.Id));
        item.Delete();
        Context.SaveChanges();
        return true;
    }
    public override async Task<bool> RemoveAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await Context.UserEntities.SingleAsync(item => item.EntityId.Equals(entityId));
        entity.Delete();
        Context.SaveChanges();
        return true;
    }
    public override async Task<bool> RemoveAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await Context.UserEntities.SingleAsync(item => item.Id == id);
        entity.Delete();
        Context.SaveChanges();
        return true;
    }


}