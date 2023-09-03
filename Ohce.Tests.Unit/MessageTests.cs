﻿namespace Ohce.Tests.Unit;

public class MessageTests
{
    [Fact]
    public void Message_IsNotNull()
    {
        var message = new Message();

        message.Should().NotBeNull();
    }

    [Theory]
    [InlineData(6)]
    [InlineData(11)]
    public void GetWelcomeMessage_WhenTimeIsMorningHours_ReturnGoodMorningMessage(int hour)
    {
        var message = new Message();

        var result = message.GetWelcomeMessage(hour, "Kunal");

        result.Should().NotBeNull();
        result.Should().Be("¡Buenos días Kunal!");
    }

    [Theory]
    [InlineData(12)]
    [InlineData(19)]
    public void GetWelcomeMessage_WhenTimeIsAfternoonHours_ReturnGoodAfternoonMessage(int hour)
    {
        var message = new Message();

        var result = message.GetWelcomeMessage(hour, "Kunal");

        result.Should().NotBeNull();
        result.Should().Be("¡Buenas tardes Kunal!");
    }

    [Theory]
    [InlineData(20)]
    [InlineData(5)]
    public void GetWelcomeMessage_WhenTimeIsNightHours_ReturnGoodNightMessage(int hours)
    {
        var message = new Message();

        var result = message.GetWelcomeMessage(hours, "Kunal");

        result.Should().NotBeNull();
        result.Should().Be("¡Buenas noches Kunal!");
    }
}