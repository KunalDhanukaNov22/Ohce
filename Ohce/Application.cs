public class Application : IApplication
{
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
    }
}