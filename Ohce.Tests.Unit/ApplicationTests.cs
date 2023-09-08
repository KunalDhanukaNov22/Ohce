using System.Text;
using Moq;

namespace Ohce.Tests.Unit;

public class ApplicationTests
{
    private Mock<TextReader> consoleInput;
    private Mock<ICurrentHour> currentHour;
    private IMessage message;
    private StringBuilder consoleOutput;
    private Application application;

    public ApplicationTests()
    {
        message = new Message();
        currentHour = new Mock<ICurrentHour>();
        application = new Application(message, currentHour.Object);

        consoleInput = new Mock<TextReader>();
        Console.SetIn(consoleInput.Object);

        consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        Console.SetOut(consoleOutputWriter);
    }

    [Fact]
    public void Application_IsNotNull()
    {
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

        var output = () => application.Run();

        output.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Run_WhenNameIsProvidedInMorning_GreetWithMorningWelcomeMessage()
    {
        consoleInput
         .Setup(x => x.ReadLine())
         .Returns("Kunal");

        currentHour.Setup(x => x.Get()).Returns(8);

        application.Run();

        var output = consoleOutput.ToString().Trim();

        output.Should().Be("¡Buenos días Kunal!");

    }

    [Fact]
    public void Run_WhenNameIsProvidedInAfternoon_GreetWithAfternoonWelcomeMessage()
    {
        consoleInput
         .Setup(x => x.ReadLine())
         .Returns("Kunal");

        currentHour.Setup(x => x.Get()).Returns(13);

        application.Run();

        var output = consoleOutput.ToString().Trim();

        output.Should().Be("¡Buenas tardes Kunal!");

    }

    [Fact]
    public void Run_WhenNameIsProvidedInNight_GreetWithNightWelcomeMessage()
    {
        consoleInput
         .Setup(x => x.ReadLine())
         .Returns("Kunal");

        currentHour.Setup(x => x.Get()).Returns(20);

        application.Run();

        var output = consoleOutput.ToString().Trim();

        output.Should().Be("¡Buenas noches Kunal!");

    }
}