namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void DateOverloads_ValidValues_NoProblems()
    {
        // Arrange
        var todayDateTime = DateTime.Today.AddHours(12);
        var todayOffset = new DateTimeOffset(DateTime.Today.AddHours(12));
        var today = DateOnly.FromDateTime(DateTime.Today);

        // Act
        var set = Rules.Set()
            .InPast(todayDateTime.AddDays(-1))
            .InPast(todayOffset.AddDays(-1))
            .InPast(today.AddDays(-1))
            .InFuture(todayDateTime.AddDays(1))
            .InFuture(todayOffset.AddDays(1))
            .InFuture(today.AddDays(1))
            .Today(todayDateTime)
            .Today(todayOffset)
            .Today(today)
            .After(todayDateTime, todayDateTime.AddDays(-1))
            .After(todayOffset, todayOffset.AddDays(-1))
            .After(today, today.AddDays(-1))
            .Before(todayDateTime, todayDateTime.AddDays(1))
            .Before(todayOffset, todayOffset.AddDays(1))
            .Before(today, today.AddDays(1))
            .Between(todayDateTime, todayDateTime.AddDays(-1), todayDateTime.AddDays(1))
            .Between(todayOffset, todayOffset.AddDays(-1), todayOffset.AddDays(1))
            .Between(today, today.AddDays(-1), today.AddDays(1));

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void InPast_Future_ProducesProblem()
    {
        // Arrange
        var value = DateTime.Today.AddDays(1);

        // Act
        var set = Rules.Set().InPast(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.InPast, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void InFuture_Past_ProducesProblem()
    {
        // Arrange
        var value = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);

        // Act
        var set = Rules.Set().InFuture(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.InFuture, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Today_Yesterday_ProducesProblem()
    {
        // Arrange
        var value = new DateTimeOffset(DateTime.Today.AddDays(-1));

        // Act
        var set = Rules.Set().Today(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Today, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void After_NotAfter_ProducesProblemWithExpectedValue()
    {
        // Arrange
        var value = new DateTime(2025, 1, 1);
        var compareTo = new DateTime(2025, 1, 2);

        // Act
        var set = Rules.Set().After(value, compareTo);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.After, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(compareTo, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void Before_NotBefore_ProducesProblemWithExpectedValue()
    {
        // Arrange
        var value = new DateOnly(2025, 1, 2);
        var compareTo = new DateOnly(2025, 1, 1);

        // Act
        var set = Rules.Set().Before(value, compareTo);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Before, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(compareTo, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void Between_OutsideRange_ProducesProblemWithExpectedRange()
    {
        // Arrange
        var value = new DateTimeOffset(2025, 1, 4, 0, 0, 0, TimeSpan.Zero);
        var start = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2025, 1, 3, 0, 0, 0, TimeSpan.Zero);

        // Act
        var set = Rules.Set().Between(value, start, end);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Between, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
        var expected = Assert.IsType<DateTimeOffset[]>(p.Extensions[Rules.ExpectedValueProperty]);
        Assert.Equal([start, end], expected);
    }
}
