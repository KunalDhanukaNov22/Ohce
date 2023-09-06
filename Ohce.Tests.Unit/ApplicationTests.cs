namespace Ohce.Tests.Unit;

public class ApplicationTests
{
    [Fact]
    public void Application_IsNotNull()
    {
        var app = new Application();
        app.Should().NotBeNull();
    }
}