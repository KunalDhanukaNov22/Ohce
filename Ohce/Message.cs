public class Message
{
    private readonly ICurrentHour currentHour;

    public Message(ICurrentHour currentHour)
    {
        this.currentHour = currentHour;
    }

    public string GetWelcomeMessage(string name)
    {
        var hour = currentHour.Get();

        if (hour < 0 || hour > 23)
            throw new ArgumentOutOfRangeException(nameof(hour));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        if (hour >= 6 && hour < 12)
            return $"¡Buenos días {name}!";
        else if (hour >= 12 && hour < 20)
            return $"¡Buenas tardes {name}!";
        else
            return $"¡Buenas noches {name}!";
    }
}