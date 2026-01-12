namespace RoyalCode.SmartValidations.Tests.Predicates;

public class BuildInRulesTests
{
    [Theory]
    [MemberData(nameof(NotNull_Data))]
    public void NotNull<T>(T value, bool expectProblems)
    {
        // Arrange
        var rules = Rules.Set().NotNull(value);

        // Act
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.Equal(expectProblems, hasProblems);
        if (hasProblems)
            Assert.Single(problems!);
        else
            Assert.Null(problems);
    }

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

    public static IEnumerable<object?[]> NotNull_Data()
    {
        yield return ["Hello", false];
        yield return ["", false];
        yield return [null, true];
        yield return [1, false];
        yield return [0, false];
    }
}