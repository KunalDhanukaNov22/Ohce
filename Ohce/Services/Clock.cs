public class Clock : IClock
{
    public DateTime GetCurrentDateTimeNowUtc() => DateTime.UtcNow;
}