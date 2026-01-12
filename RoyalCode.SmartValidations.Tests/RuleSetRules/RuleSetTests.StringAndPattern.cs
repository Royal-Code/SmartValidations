using System.Text.RegularExpressions;

namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void Matches_PatternMismatch_ProducesProblem()
    {
        // Arrange
        string? value = "abc";
        var pattern = "^[0-9]+$";

        // Act
        var set = Rules.Set().Matches(value, pattern);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.MatchPattern, p.Extensions[Rules.RuleProperty]);
        Assert.Equal(pattern, p.Extensions[Rules.PatternProperty]);
        Assert.Equal("abc", p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Matches_PatternMatch_NoProblems()
    {
        // Arrange
        string? value = "123";
        var pattern = "^[0-9]+$";

        // Act
        var set = Rules.Set().Matches(value, pattern);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Matches_RegexOverload_Mismatch_ProducesProblem()
    {
        // Arrange
        string? value = "abc";
        var regex = new Regex("^[0-9]+$", RegexOptions.Compiled);

        // Act
        var set = Rules.Set().Matches(value, regex, "digits only");

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.MatchPattern, p.Extensions[Rules.RuleProperty]);
        Assert.Equal(regex.ToString(), p.Extensions[Rules.PatternProperty]);
        Assert.Equal("abc", p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void NotMatches_PatternMatch_ProducesProblem()
    {
        // Arrange
        string? value = "abc";
        var pattern = "^[a-z]+$";

        // Act
        var set = Rules.Set().NotMatches(value, pattern);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.NotMatchPattern, p.Extensions[Rules.RuleProperty]);
        Assert.Equal(pattern, p.Extensions[Rules.PatternProperty]);
        Assert.Equal("abc", p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void StartsWith_Mismatch_ProducesProblem()
    {
        // Arrange
        string? value = "hello";
        var expected = "He";

        // Act
        var set = Rules.Set().StartsWith(value, expected, StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.StartsWith, p.Extensions[Rules.RuleProperty]);
        Assert.Equal("hello", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(expected, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void EndsWith_Mismatch_ProducesProblem()
    {
        // Arrange
        string? value = "file.txt";
        var expected = ".json";

        // Act
        var set = Rules.Set().EndsWith(value, expected, StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.EndsWith, p.Extensions[Rules.RuleProperty]);
        Assert.Equal("file.txt", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(expected, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void Contains_MissingSubstring_ProducesProblem()
    {
        // Arrange
        string? value = "abcdef";
        var expected = "xyz";

        // Act
        var set = Rules.Set().Contains(value, expected, StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.Contains, p.Extensions[Rules.RuleProperty]);
        Assert.Equal("abcdef", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(expected, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void NotContain_ContainsUnexpected_ProducesProblem()
    {
        // Arrange
        string? value = "abcdef";
        var unexpected = "cd";

        // Act
        var set = Rules.Set().NotContain(value, unexpected, StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NotContain, p.Extensions[Rules.RuleProperty]);
        Assert.Equal("abcdef", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(unexpected, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void OnlyLetters_WithDigits_ProducesProblem()
    {
        // Arrange
        string? value = "abc123";

        // Act
        var set = Rules.Set().OnlyLetters(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.OnlyLetters, p.Extensions[Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void OnlyDigits_WithLetters_ProducesProblem()
    {
        // Arrange
        string? value = "123a";

        // Act
        var set = Rules.Set().OnlyDigits(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.OnlyDigits, p.Extensions[Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void OnlyLettersOrDigits_WithSymbol_ProducesProblem()
    {
        // Arrange
        string? value = "abc-123";

        // Act
        var set = Rules.Set().OnlyLettersOrDigits(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.OnlyLettersOrDigits, p.Extensions[Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void NoWhiteSpace_WithSpace_ProducesProblem()
    {
        // Arrange
        string? value = "has space";

        // Act
        var set = Rules.Set().NoWhiteSpace(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NoWhiteSpace, p.Extensions[Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }
}
