using Moq;

namespace Ohce.Tests.Unit;

public class MessageTests
{
    private const string ValidName = "Kunal";

    private readonly Mock<ICurrentHour> currentHour;
    private readonly Message message;

    public MessageTests()
    {
        currentHour = new Mock<ICurrentHour>();

        message = new Message(currentHour.Object);

        SetUpHourNow(2);
    }

    [Fact]
    public void Message_IsNotNull()
    {
        message.Should().NotBeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetWelcomeMessage_WhenSuppliedInvalidName_ArgumentNullExceptionIsThrown(string invalidName)
    {
        var result = () => message.GetWelcomeMessage(invalidName);

        result.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(24)]
    public void GetWelcomeMessage_WhenTimeSuppliedIsNotWithingTheRange_ArgumentOutOfRangeExceptionIsThrown(int invalidHour)
    {
        SetUpHourNow(invalidHour);

        var result = () => message.GetWelcomeMessage(ValidName);

        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(6)]
    [InlineData(11)]
    public void GetWelcomeMessage_WhenTimeIsMorningHours_ReturnGoodMorningMessage(int hour)
    {
        SetUpHourNow(hour);

        var result = message.GetWelcomeMessage(ValidName);

        result.Should().NotBeNull();
        result.Should().Be("¡Buenos días Kunal!");
    }

    [Theory]
    [InlineData(12)]
    [InlineData(19)]
    public void GetWelcomeMessage_WhenTimeIsAfternoonHours_ReturnGoodAfternoonMessage(int hour)
    {
        SetUpHourNow(hour);

        var result = message.GetWelcomeMessage(ValidName);

        result.Should().NotBeNull();
        result.Should().Be("¡Buenas tardes Kunal!");
    }

    [Theory]
    [InlineData(20)]
    [InlineData(5)]
    public void GetWelcomeMessage_WhenTimeIsNightHours_ReturnGoodNightMessage(int hour)
    {
        SetUpHourNow(hour);

        var result = message.GetWelcomeMessage(ValidName);

        result.Should().NotBeNull();
        result.Should().Be("¡Buenas noches Kunal!");
    }

    private void SetUpHourNow(int hour)
    {
        currentHour
            .Setup(x => x.Get())
            .Returns(hour);
    }
}