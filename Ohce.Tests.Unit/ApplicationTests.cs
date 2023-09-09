using System.Text;
using Moq;

namespace Ohce.Tests.Unit;

public class ApplicationTests
{
    private const string ValidName = "Kunal";
    private const string Stop = "Stop!";

    private readonly Mock<TextReader> consoleInput;
    private readonly Mock<ICurrentHour> currentHour;
    private readonly IMessage message;
    private readonly IStringReversal stringReversal;
    private readonly StringBuilder consoleOutput;
    private readonly Application application;

    public ApplicationTests()
    {
        message = new Message();
        stringReversal = new StringReversal();
        currentHour = new Mock<ICurrentHour>();
        application = new Application(message, stringReversal, currentHour.Object);

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
    [InlineData(8, "¡Buenos días Kunal!")]
    [InlineData(13, "¡Buenas tardes Kunal!")]
    [InlineData(20, "¡Buenas noches Kunal!")]
    public void Run_WhenNameIsProvidedWithTime_GreetWithWelcomeMessageAsPerTime(int hour, string message)
    {
        InputSetup(hour, "Kunal", Stop);

        application.Run();

        var outputarray = ConsoleOutput();

        outputarray[0].Should().Be(message);
    }

    [Fact]
    public void Run_WhenInputSuppliedIsStop_ReturnSignOffMessage()
    {
        InputSetup(20, ValidName, "Stop!");

        application.Run();

        var outputarray = ConsoleOutput();

        outputarray[1].Should().Be("Adios Kunal");
    }

    [Fact]
    public void Run_UntilInputSuppliedIsNotStop_ContinueToAskForUserInput()
    {
        InputSetup(20, ValidName, "String1", "String2", "String3", "String4", "String5", Stop);

        application.Run();

        var outputarray = ConsoleOutput();

        outputarray[6].Should().Be("Adios Kunal");
    }

    [Fact]
    public void Run_WhenInputSuppliedApartFromNameAndStop_GetReverseOfTheInputString()
    {
        InputSetup(20, ValidName, "London", "Paris", Stop);

        application.Run();

        var outputarray = ConsoleOutput();

        outputarray[1].Should().Be("nodnoL");
        outputarray[2].Should().Be("siraP");
        outputarray[3].Should().Be("Adios Kunal");
    }

    [Fact]
    public void Run_WhenInputSuppliedApartFromNameAndStopIsPalindrome_GetReverseOfTheInputStringWithPalindromeMessage()
    {
        InputSetup(20, ValidName, "tenet", Stop);

        application.Run();

        var outputarray = ConsoleOutput();

        outputarray[1].Should().Be("tenet");
        outputarray[2].Should().Be("¡Bonita palabra!");
        outputarray[3].Should().Be("Adios Kunal");
    }

    private void InputSetup(int hour, params string[] inputs)
    {
        var sequence = new MockSequence();

        foreach (var input in inputs)
        {
            consoleInput
                .InSequence(sequence)
                .Setup(x => x.ReadLine())
                .Returns(input);
        }

        currentHour.Setup(x => x.Get()).Returns(hour);
    }

    private string[] ConsoleOutput()
    {
        return consoleOutput
            .ToString()
            .Trim()
            .Split(Environment.NewLine);
    }
}