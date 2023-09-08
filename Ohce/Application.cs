public class Application : IApplication
{
    public void Run()
    {
        var name = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
    }
}