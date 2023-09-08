using System.Text;
using Moq;

namespace Ohce.Tests.Unit;

public class ApplicationTests
{
    private const string ValidName = "Kunal";

    private Mock<TextReader> consoleInput;
    private Mock<ICurrentHour> currentHour;
    private IMessage message;
    private IStringReversal stringReversal;
    private StringBuilder consoleOutput;
    private Application application;

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
    [InlineData("")]
    [InlineData(null)]
    public void Run_WhenNameIsEmpty_ThrowsError(string invalidName)
    {
        InputSetup(invalidName, 8);

        var output = () => application.Run();

        output.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Run_WhenNameIsProvidedInMorning_GreetWithMorningWelcomeMessage()
    {
        InputSetup(ValidName, 8);

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[0].Should().Be("¡Buenos días Kunal!");

    }

    [Fact]
    public void Run_WhenNameIsProvidedInAfternoon_GreetWithAfternoonWelcomeMessage()
    {
        InputSetup(ValidName, 13);

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[0].Should().Be("¡Buenas tardes Kunal!");

    }

    [Fact]
    public void Run_WhenNameIsProvidedInNight_GreetWithNightWelcomeMessage()
    {
        InputSetup(ValidName, 20);

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[0].Should().Be("¡Buenas noches Kunal!");
    }

    [Fact]
    public void Run_WhenInputSuppliedIsStop_ReturnSignOffMessage()
    {
        InputSetup(ValidName, 20);

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[1].Should().Be("Adios Kunal");
    }

    [Fact]
    public void Run_UntilInputSuppliedIsNotStop_ContinueToAskForUserInput()
    {
        var sequence = new MockSequence();

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns(ValidName);

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("String1");

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("String2");

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("String3");

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("Stop!");

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[4].Should().Be("Adios Kunal");
    }

    [Fact]
    public void Run_WhenInputSuppliedApartFromNameAndStop_GetReverseOfTheInputString()
    {
        var sequence = new MockSequence();

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns(ValidName);

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("String1");

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("London");

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("Paris");

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("Stop!");

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[1].Should().Be("1gnirtS");
        outputarray[2].Should().Be("nodnoL");
        outputarray[3].Should().Be("siraP");
        outputarray[4].Should().Be("Adios Kunal");
    }

    [Fact]
    public void Run_WhenInputSuppliedApartFromNameAndStopIsPalindrome_GetReverseOfTheInputStringWithPalindromeMessage()
    {
        var sequence = new MockSequence();

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns(ValidName);

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("tenet");

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("Stop!");

        application.Run();

        var outputarray = consoleOutput.ToString().Trim().Split(Environment.NewLine);

        outputarray[1].Should().Be("tenet");
        outputarray[2].Should().Be("¡Bonita palabra!");
        outputarray[3].Should().Be("Adios Kunal");
    }


    private void InputSetup(string name, int hour)
    {
        var sequence = new MockSequence();

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns(name);

        consoleInput
            .InSequence(sequence)
            .Setup(x => x.ReadLine())
            .Returns("Stop!");

        currentHour.Setup(x => x.Get()).Returns(hour);
    }
}