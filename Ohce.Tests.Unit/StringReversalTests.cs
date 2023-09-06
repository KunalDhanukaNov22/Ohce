namespace Ohce.Tests.Unit;

public class StringReversalTests
{
    [Fact]
    public void StringReversal_IsNotNull()
    {
        var result = new StringReversal();

        result.Should().NotBeNull();
    }

    [Fact]
    public void Reverse_WhenInvokedWithInvalidStringInput_ThrowsException()
    {
        var stringReversal = new StringReversal();

        var result = () => stringReversal.Reverse(string.Empty);

        result.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData("sample", "elpmas", false)]
    [InlineData("ready", "ydaer", false)]
    [InlineData("oto", "oto", true)]
    [InlineData("Tenet", "teneT", true)]
    public void Reverse_WhenInvokedWithValidStringInput_ReturnReversedStringWithIsPalindromeStatus(string inputString, string expectedString, bool isPalindrome)
    {
        var stringReversal = new StringReversal();

        var result = stringReversal.Reverse(inputString);

        result.ReversedString.Should().Be(expectedString);
        result.IsPalindrome.Should().Be(isPalindrome);
    }
}