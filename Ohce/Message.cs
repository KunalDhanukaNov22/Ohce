public class Message : IMessage
{
    private readonly Person person;

    public Message() => person = new Person();

    public string GetWelcomeMessage(string name, int hour)
    {
        if (hour < 0 || hour > 23)
            throw new ArgumentOutOfRangeException(nameof(hour));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        person.Name = name;

        if (hour >= 6 && hour < 12)
            return $"¡Buenos días {name}!";
        else if (hour >= 12 && hour < 20)
            return $"¡Buenas tardes {name}!";
        else
            return $"¡Buenas noches {name}!";
    }

    public string GetPalindromeMessage()
    {
        return "¡Bonita palabra!";
    }

    public string GetSignOffMessage()
    {
        return $"Adios {person.Name}";
    }
}