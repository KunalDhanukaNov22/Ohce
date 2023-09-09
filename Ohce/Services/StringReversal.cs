using System.Text;

public class StringReversal : IStringReversal
{
    public Result Reverse(string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText))
            throw new ArgumentNullException(nameof(inputText));

        var stringBuilder = new StringBuilder();
        var result = new Result();

        for (int index = inputText.Length - 1; index >= 0; index--)
        {
            stringBuilder.Append(inputText[index]);
        }

        result.ReversedString = stringBuilder.ToString();
        result.IsPalindrome = result.ReversedString.ToLower() == inputText.ToLower();

        return result;
    }
}