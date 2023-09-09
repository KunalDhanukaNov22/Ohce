public static class Constants
{
    public const string PalindromeMessage = "¡Bonita palabra!";
    public const string Stop = "Stop!";

    public static string MorningWelcomeMessage(string name) => $"¡Buenos días {name}!";
    public static string AfternoonWelcomeMessage(string name) => $"¡Buenas tardes {name}!";
    public static string EveningWelcomeMessage(string name) => $"¡Buenas noches {name}!";

    public static string SignOffMessage(string name) => $"Adios {name}";
}