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
    [InlineData("sample", "elpmas")]
    [InlineData("ready", "ydaer")]
    public void Reverse_WhenInvokedWithValidStringInput_ReturnReversedString(string inputString, string expectedString)
    {
        var stringReversal = new StringReversal();

        var result = stringReversal.Reverse(inputString);

        result.Should().Be(expectedString);
    }
}