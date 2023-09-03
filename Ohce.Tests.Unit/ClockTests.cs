namespace Ohce.Tests.Unit;

public class ClockTests
{
    private readonly IClock clock = new Clock();

    [Fact]
    public void Clock_IsNotNull()
    {
        clock.Should().NotBeNull();
    }

    [Fact]
    public void GetCurrentDateTimeNowUtc_ReturnCurrentDateTimeInUtc()
    {
        var result = clock.GetCurrentDateTimeNowUtc();

        result.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(3));
    }

}