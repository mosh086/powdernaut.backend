using Powdernaut.Domain.Common;
using Powdernaut.Infrastructure.Data.Conversions;

namespace Powdernaut.Infrastructure.Tests;

public class EntityIdConversionTests
{
    [Fact]
    public void ConvertToProvider_AndConvertFromProvider_PreserveGuid()
    {
        var value = Guid.Parse("b99fc73f-9a9e-40ea-8144-43cde44c9e6d");
        var entityId = EntityId.FromGuid(value);
        var conversion = new EntityIdConversion();

        var providerValue = (Guid)conversion.ConvertToProvider(entityId)!;
        var convertedEntityId = (EntityId)conversion.ConvertFromProvider(providerValue)!;

        Assert.Equal(value, providerValue);
        Assert.Equal(entityId, convertedEntityId);
    }
}
