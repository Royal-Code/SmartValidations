namespace RoyalCode.SmartValidations.Tests.Predicates;

public partial class BuildInPredicatesTests
{
    [Fact]
    public void DateTime_RelativeDatePredicates_ReturnExpected()
    {
        // Arrange
        var today = DateTime.Today;

        // Act
        // Assert
        Assert.True(BuildInPredicates.InPast(today.AddDays(-1)));
        Assert.False(BuildInPredicates.InPast(today.AddDays(1)));
        Assert.True(BuildInPredicates.InFuture(today.AddDays(1)));
        Assert.False(BuildInPredicates.InFuture(today.AddDays(-1)));
        Assert.True(BuildInPredicates.Today(today.AddHours(12)));
        Assert.False(BuildInPredicates.Today(today.AddDays(-1)));
    }

    [Fact]
    public void DateTimeOffset_RelativeDatePredicates_ReturnExpected()
    {
        // Arrange
        var today = new DateTimeOffset(DateTime.Today.AddHours(12));

        // Act
        // Assert
        Assert.True(BuildInPredicates.InPast(today.AddDays(-1)));
        Assert.False(BuildInPredicates.InPast(today.AddDays(1)));
        Assert.True(BuildInPredicates.InFuture(today.AddDays(1)));
        Assert.False(BuildInPredicates.InFuture(today.AddDays(-1)));
        Assert.True(BuildInPredicates.Today(today));
        Assert.False(BuildInPredicates.Today(today.AddDays(-1)));
    }

    [Fact]
    public void DateOnly_RelativeDatePredicates_ReturnExpected()
    {
        // Arrange
        var today = DateOnly.FromDateTime(DateTime.Today);

        // Act
        // Assert
        Assert.True(BuildInPredicates.InPast(today.AddDays(-1)));
        Assert.False(BuildInPredicates.InPast(today.AddDays(1)));
        Assert.True(BuildInPredicates.InFuture(today.AddDays(1)));
        Assert.False(BuildInPredicates.InFuture(today.AddDays(-1)));
        Assert.True(BuildInPredicates.Today(today));
        Assert.False(BuildInPredicates.Today(today.AddDays(-1)));
    }

    [Fact]
    public void DateTime_CompareDatePredicates_ReturnExpected()
    {
        // Arrange
        var start = new DateTime(2025, 1, 1);
        var value = new DateTime(2025, 1, 2);
        var end = new DateTime(2025, 1, 3);

        // Act
        // Assert
        Assert.True(BuildInPredicates.After(value, start));
        Assert.False(BuildInPredicates.After(start, value));
        Assert.True(BuildInPredicates.Before(value, end));
        Assert.False(BuildInPredicates.Before(end, value));
        Assert.True(BuildInPredicates.Between(value, start, end));
        Assert.False(BuildInPredicates.Between(start.AddDays(-1), start, end));
    }

    [Fact]
    public void DateTimeOffset_CompareDatePredicates_ReturnExpected()
    {
        // Arrange
        var start = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var value = new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2025, 1, 3, 0, 0, 0, TimeSpan.Zero);

        // Act
        // Assert
        Assert.True(BuildInPredicates.After(value, start));
        Assert.False(BuildInPredicates.After(start, value));
        Assert.True(BuildInPredicates.Before(value, end));
        Assert.False(BuildInPredicates.Before(end, value));
        Assert.True(BuildInPredicates.Between(value, start, end));
        Assert.False(BuildInPredicates.Between(start.AddDays(-1), start, end));
    }

    [Fact]
    public void DateOnly_CompareDatePredicates_ReturnExpected()
    {
        // Arrange
        var start = new DateOnly(2025, 1, 1);
        var value = new DateOnly(2025, 1, 2);
        var end = new DateOnly(2025, 1, 3);

        // Act
        // Assert
        Assert.True(BuildInPredicates.After(value, start));
        Assert.False(BuildInPredicates.After(start, value));
        Assert.True(BuildInPredicates.Before(value, end));
        Assert.False(BuildInPredicates.Before(end, value));
        Assert.True(BuildInPredicates.Between(value, start, end));
        Assert.False(BuildInPredicates.Between(start.AddDays(-1), start, end));
    }
}
