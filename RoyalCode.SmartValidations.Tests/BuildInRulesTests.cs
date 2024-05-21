namespace RoyalCode.SmartValidations.Tests;

public class BuildInRulesTests
{
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData("1", false)]
    public void String_NotEmpty(string? value, bool expectProblems)
    {
        // Arrange
        var rules = Rules.Set().NotEmpty(value);
        
        // Act
        var hasProblems = rules.HasProblems(out var problems);
        
        // Assert
        Assert.Equal(expectProblems, hasProblems);
        if (hasProblems)
            Assert.Single(problems!);
        else
            Assert.Null(problems);
    }
}