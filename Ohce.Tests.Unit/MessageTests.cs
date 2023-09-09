namespace Ohce.Tests.Unit;

public class MessageTests
{
    private const string ValidName = "Kunal";
    private const int ValidHour = 8;
    private readonly Message message = new();

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
        var result = () => message.GetWelcomeMessage(invalidName, ValidHour);

        result.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(24)]
    public void GetWelcomeMessage_WhenTimeSuppliedIsNotWithingTheRange_ArgumentOutOfRangeExceptionIsThrown(int invalidHour)
    {
        var result = () => message.GetWelcomeMessage(ValidName, invalidHour);

        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(6)]
    [InlineData(11)]
    public void GetWelcomeMessage_WhenTimeIsMorningHours_ReturnGoodMorningMessage(int hour)
    {
        var result = message.GetWelcomeMessage(ValidName, hour);

        ValidateResult(result, "¡Buenos días Kunal!");
    }

    [Theory]
    [InlineData(12)]
    [InlineData(19)]
    public void GetWelcomeMessage_WhenTimeIsAfternoonHours_ReturnGoodAfternoonMessage(int hour)
    {
        var result = message.GetWelcomeMessage(ValidName, hour);

        ValidateResult(result, "¡Buenas tardes Kunal!");
    }

    [Theory]
    [InlineData(20)]
    [InlineData(5)]
    public void GetWelcomeMessage_WhenTimeIsNightHours_ReturnGoodNightMessage(int hour)
    {
        var result = message.GetWelcomeMessage(ValidName, hour);

        ValidateResult(result, "¡Buenas noches Kunal!");
    }

    [Fact]
    public void GetPalindromeMessage_WhenInvoked_ThenReturnPalindromeMessage()
    {
        var result = message.GetPalindromeMessage();

        ValidateResult(result, "¡Bonita palabra!");
    }

    [Fact]
    public void GetSignOffMessage_WhenInvoked_ThenReturnSignOffMessage()
    {
        message.GetWelcomeMessage(ValidName, ValidHour);

        var result = message.GetSignOffMessage();

        ValidateResult(result, "Adios Kunal");
    }

    private void ValidateResult(string actualMessage, string expectedMessage)
    {
        actualMessage.Should().NotBeNull();
        actualMessage.Should().Be(expectedMessage);
    }
}