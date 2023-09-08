using System.Text;
using Moq;

namespace Ohce.Tests.Unit;

public class ApplicationTests
{
    private const string ValidName = "Kunal";

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
    public void Run_WhenNameIsEmpty_ThrowsError(string invalidName)
    {
        WelcomeMessageSetup(invalidName, 8);

        var output = () => application.Run();

        output.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Run_WhenNameIsProvidedInMorning_GreetWithMorningWelcomeMessage()
    {
        WelcomeMessageSetup(ValidName, 8);

        application.Run();

        var output = consoleOutput.ToString().Trim();

        output.Should().Be("¡Buenos días Kunal!");

    }

    [Fact]
    public void Run_WhenNameIsProvidedInAfternoon_GreetWithAfternoonWelcomeMessage()
    {
        WelcomeMessageSetup(ValidName, 13);

        application.Run();

        var output = consoleOutput.ToString().Trim();

        output.Should().Be("¡Buenas tardes Kunal!");

    }

    [Fact]
    public void Run_WhenNameIsProvidedInNight_GreetWithNightWelcomeMessage()
    {
        WelcomeMessageSetup(ValidName, 20);

        application.Run();

        var output = consoleOutput.ToString().Trim();

        output.Should().Be("¡Buenas noches Kunal!");
    }

    [Fact]
    public void Run_WhenInputSuppliedIsStop_ReturnSignOffMessage()
    {
        var sequence = new MockSequence();

        consoleInput
         .InSequence(sequence)
         .Setup(x => x.ReadLine())
         .Returns(ValidName);

        consoleInput
         .InSequence(sequence)
         .Setup(x => x.ReadLine())
         .Returns("Stop!");

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[1].Should().Be("Adios Kunal");
    }


    private void WelcomeMessageSetup(string name, int hour)
    {
        consoleInput
         .Setup(x => x.ReadLine())
         .Returns(name);

        currentHour.Setup(x => x.Get()).Returns(hour);
    }
}