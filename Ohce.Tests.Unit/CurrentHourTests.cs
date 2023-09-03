using Moq;

namespace Ohce.Tests.Unit;

public class CurrentHourTests
{
    private readonly Mock<IClock> clock = new();
    private readonly CurrentHour currentHour;

    public CurrentHourTests()
    {
        currentHour = new CurrentHour(clock.Object);
    }

    [Fact]
    public void CurrentHour_IsNotNull()
    {
        currentHour.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1, 00, 00, 001, 1)]
    [InlineData(10, 59, 59, 999, 10)]
    [InlineData(23, 59, 59, 999, 23)]
    [InlineData(0, 30, 59, 0, 0)]
    public void Get_GivenADateTime_ReturnHourFromDateTime(int actualHour, int actualMinutes, int actualSeconds, int actualMilliSeconds, int expectedHour)
    {
        var actualDateTime = new DateTime(2023, 09, 03, actualHour, actualMinutes, actualSeconds, actualMilliSeconds, DateTimeKind.Utc);

        clock
            .Setup(x => x.GetCurrentDateTimeNowUtc())
            .Returns(actualDateTime);

        var result = currentHour.Get();

        result.Should().Be(expectedHour);
        clock.Verify(x => x.GetCurrentDateTimeNowUtc(), Times.Once);
    }
}