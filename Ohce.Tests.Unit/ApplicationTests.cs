using System.Text;
using Moq;

namespace Ohce.Tests.Unit;

public class ApplicationTests
{
    private Mock<TextReader> consoleInput;

    public ApplicationTests()
    {
        consoleInput = new Mock<TextReader>();
        Console.SetIn(consoleInput.Object);

        var consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        Console.SetOut(consoleOutputWriter);

    }

    [Fact]
    public void Application_IsNotNull()
    {
        var application = new Application();
        application.Should().NotBeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Run_WhenNameIsEmpty_ThrowsError(string userInput)
    {
        consoleInput
         .Setup(x => x.ReadLine())
         .Returns(userInput);

        var application = new Application();
        var output = () => application.Run();

        output.Should().Throw<ArgumentNullException>();
    }
}