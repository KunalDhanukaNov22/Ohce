public class Application : IApplication
{
    private const string Stop = "Stop!";

    private readonly IMessage message;
    private readonly ICurrentHour currentHour;

    public Application(IMessage message, ICurrentHour currentHour)
    {
        this.message = message;
        this.currentHour = currentHour;
    }

    public void Run()
    {
        var name = Console.ReadLine()?.Trim();

        var welcomeMessage = message.GetWelcomeMessage(name, currentHour.Get());

        Console.WriteLine(welcomeMessage);

        var input = Console.ReadLine()?.Trim();

        if (input == Stop)
            Console.WriteLine(message.GetSignOffMessage());
    }
}