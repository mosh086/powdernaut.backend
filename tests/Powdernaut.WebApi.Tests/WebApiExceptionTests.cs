using Powdernaut.WebApi.Exceptions;

namespace Powdernaut.WebApi.Tests;

public class WebApiExceptionTests
{
    [Fact]
    public void ToString_ReplacesMessageParameters()
    {
        var exception = new WebApiException("User {0} was not found", "Alex");

        var result = exception.ToString();

        Assert.Equal("User Alex was not found", result);
    }
}
