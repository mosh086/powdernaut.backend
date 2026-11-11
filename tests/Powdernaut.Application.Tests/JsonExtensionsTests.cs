using Powdernaut.Application.Utilities.Extensions;

namespace Powdernaut.Application.Tests;

public class JsonExtensionsTests
{
    [Fact]
    public void ToJson_UsesCamelCasePropertyNames()
    {
        var result = new JsonTestModel { DisplayName = "Example" }.ToJson();

        Assert.Equal("{\"displayName\":\"Example\"}", result);
    }

    [Fact]
    public void To_DeserializesCaseInsensitivePropertyNames()
    {
        var result = "{\"displayName\":\"Example\"}".To<JsonTestModel>();

        Assert.Equal("Example", result.DisplayName);
    }

    public sealed class JsonTestModel
    {
        public string DisplayName { get; set; } = string.Empty;
    }
}
