﻿public class Message
{
    public string GetWelcomeMessage(int hour, string name)
    {
        if (hour >= 6 && hour < 12)
            return $"¡Buenos días {name}!";
        else if (hour >= 12 && hour < 20)
            return $"¡Buenas tardes {name}!";
        else
            return $"¡Buenas noches {name}!";
    }
}