using Powdernaut.Application.Providers.CacheSystem;
using Powdernaut.Application.Providers.DataDapper;
using Powdernaut.Application.Providers.ObjectMapper;
using Powdernaut.Application.Providers.Serializer.Objects;
using Powdernaut.Application.Providers.UserManagement;

namespace Powdernaut.Application.Providers;

public class ProviderServices
{
    public readonly IObjectSerializer Convertor;
    public readonly IObjectMapper Mapper;
    public readonly ICacheAdapter CacheAdapter;
    public readonly IMediator Mediator;
    public readonly IDataDapper DataDapper;
    public readonly IUser User;
    public ProviderServices(
        IObjectSerializer convertor,
        IObjectMapper mapper,
        ICacheAdapter cacheAdapter,
        IMediator mediator,
        IUser user,
        IDataDapper dataDapper)
    {
        Convertor = convertor;
        Mapper = mapper;
        CacheAdapter = cacheAdapter;
        Mediator = mediator;
        User = user;
        DataDapper = dataDapper;
    }
}
