namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void NotNull_StringValue_NoProblems()
    {
        // Arrange
        var value = "ok";

        // Act
        var set = Rules.Set().NotNull(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NotNull_StringNull_ProducesProblem_WithRuleAndProperty()
    {
        // Arrange
        string? value = null;

        // Act
        var set = Rules.Set<NotNullSample>().NotNull(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);

        // Property captured by CallerArgumentExpression
        Assert.Equal(nameof(value), problem.Property);

        // Display name resolution is exercised in RuleSetTests.cs; here we focus on extensions and property

        // Rule extension should be set
        Assert.True(problem.Extensions!.TryGetValue(Rules.RuleProperty, out var rule));
        Assert.Equal(Rules.NotNullOrNotEmpty, rule);

        // Should NOT have current value extension for NotNull (only rule and message)
        Assert.False(problem.Extensions.ContainsKey(Rules.CurrentValueProperty));
    }

    [Fact]
    public void NotNull_WithPropertyPrefix_RemovesPrefixFromProblemProperty()
    {
        // Arrange
        string? street = null;
        var set = Rules.Set<NotNullAddress>().WithPropertyPrefix("addr");

        // Act
        set = set.NotNull(street);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);

        // Property name should not include the prefix
        Assert.Equal(nameof(street), problem.Property);

        // Display name resolution validated elsewhere; focus on property and extensions

        // Rule extension
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }
}

file class NotNullSample
{
    [System.ComponentModel.DisplayName("Name Label")]
    public string? Name { get; set; }
}

file class NotNullAddress
{
    [System.ComponentModel.DisplayName("Street Label")]
    public string? Street { get; set; }
}
