public class Clock : IClock
{
    public DateTime GetCurrentDateTimeNowUtc()
    {
        return DateTime.UtcNow;
    }
}