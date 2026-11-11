using Powdernaut.Domain.Common;
using Powdernaut.Domain.Exceptions;

namespace Powdernaut.Domain.Tests;

public class EntityIdTests
{
    [Fact]
    public void FromGuid_PreservesValue()
    {
        var value = Guid.NewGuid();

        var entityId = EntityId.FromGuid(value);

        Assert.Equal(value, entityId.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("not-a-guid")]
    public void FromString_WhenValueIsMissingOrInvalid_ThrowsDomainException(string value)
    {
        Assert.Throws<DomainException>(() => EntityId.FromString(value));
    }

    [Fact]
    public void EntityIds_WithSameGuid_AreEqual()
    {
        var value = Guid.NewGuid();
        var first = EntityId.FromGuid(value);
        var second = EntityId.FromString(value.ToString());

        Assert.Equal(first, second);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }
}
