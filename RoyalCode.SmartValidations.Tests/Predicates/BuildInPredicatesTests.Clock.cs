namespace RoyalCode.SmartValidations.Tests.Predicates;

public partial class BuildInPredicatesTests
{
    [Fact]
    public void Clock_CanBeReplaced_ForDeterministicDateRules()
    {
        // Arrange
        var now = DateTimeOffset.Now;
        var original = BuildInPredicates.Clock;
        BuildInPredicates.Clock = new FixedTimeProvider(now);

        try
        {
            // Act
            // Assert
            Assert.True(BuildInPredicates.InPast(now.AddSeconds(-1)));
            Assert.False(BuildInPredicates.InPast(now.AddSeconds(1)));
            Assert.True(BuildInPredicates.InFuture(now.AddSeconds(1)));
            Assert.False(BuildInPredicates.InFuture(now.AddSeconds(-1)));
            Assert.True(BuildInPredicates.Today(now));

            Assert.True(BuildInPredicates.InPast(now.DateTime.AddSeconds(-1)));
            Assert.True(BuildInPredicates.InFuture(now.DateTime.AddSeconds(1)));
            Assert.True(BuildInPredicates.Today(now.DateTime));

            var today = DateOnly.FromDateTime(now.LocalDateTime);
            Assert.True(BuildInPredicates.InPast(today.AddDays(-1)));
            Assert.True(BuildInPredicates.InFuture(today.AddDays(1)));
            Assert.True(BuildInPredicates.Today(today));
        }
        finally
        {
            BuildInPredicates.Clock = original;
        }
    }

    [Fact]
    public void Today_DateTimeOffset_ComparesInClockLocalTimeZone()
    {
        // Arrange
        var now = DateTimeOffset.Now;
        var original = BuildInPredicates.Clock;
        BuildInPredicates.Clock = new FixedTimeProvider(now);

        try
        {
            // Act
            // Assert
            // the same instant expressed in another offset is still "today"
            Assert.True(BuildInPredicates.Today(now.ToUniversalTime()));
        }
        finally
        {
            BuildInPredicates.Clock = original;
        }
    }
}

file sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
{
    public override DateTimeOffset GetUtcNow() => now.ToUniversalTime();
}
