public interface IMessage
{
    string GetPalindromeMessage();
    string GetSignOffMessage();
    string GetWelcomeMessage(string name, int hour);
}