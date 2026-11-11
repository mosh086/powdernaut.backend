using Powdernaut.Application.Utilities.Extensions;

namespace Powdernaut.Application.Tests;

public class StringExtensionsTests
{
    [Fact]
    public void ToSafeNullableLong_ReturnsParsedValue()
    {
        var result = "12345".ToSafeNullableLong();

        Assert.Equal(12345L, result);
    }

    [Fact]
    public void ToSafeNullableLong_ReturnsNullForInvalidValue()
    {
        var result = "not-a-number".ToSafeNullableLong();

        Assert.Null(result);
    }
}
