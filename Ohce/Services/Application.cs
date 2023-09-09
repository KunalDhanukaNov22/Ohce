public class Application : IApplication
{
    private readonly IMessage message;
    private readonly IStringReversal stringReversal;
    private readonly ICurrentHour currentHour;

    public Application(
        IMessage message,
        IStringReversal stringReversal,
        ICurrentHour currentHour)
    {
        this.message = message;
        this.stringReversal = stringReversal;
        this.currentHour = currentHour;
    }

    public void Run()
    {
        var name = Console.ReadLine()?.Trim();

        var welcomeMessage = message.GetWelcomeMessage(name, currentHour.Get());

        Console.WriteLine(welcomeMessage);

        while (true)
        {
            var input = Console.ReadLine()?.Trim();

            if (input == Constants.Stop)
                break;

            var result = stringReversal.Reverse(input);

            Console.WriteLine(result.ReversedString);

            if (result.IsPalindrome)
                Console.WriteLine(message.GetPalindromeMessage());
        }

        Console.WriteLine(message.GetSignOffMessage());
    }
}