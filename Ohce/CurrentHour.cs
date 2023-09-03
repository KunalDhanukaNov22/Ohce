public class CurrentHour : ICurrentHour
{
    private readonly IClock clock;

    public CurrentHour(IClock clock)
    {
        this.clock = clock;
    }

    public int Get()
    {
        return clock.GetCurrentDateTimeNowUtc().Hour;
    }
}
