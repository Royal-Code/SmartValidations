namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void Must_Value_SatisfiesPredicate_NoProblems()
    {
        // Arrange
        int value = 10;
        bool Predicate(int v) => v > 5;
        string Formatter(string display, int v) => $"{display}: {v}";

        // Act
        var set = Rules.Set().Must(value, Predicate, Formatter, "custom.must");

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Must_Value_FailsPredicate_ProducesProblem_WithRuleAndCurrent()
    {
        // Arrange
        int value = 3;
        bool Predicate(int v) => v > 5;
        string Formatter(string display, int v) => $"{display}: {v}";

        // Act
        var set = Rules.Set().Must(value, Predicate, Formatter, "custom.must");

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal("custom.must", p.Extensions![Rules.RuleProperty]);
        Assert.Equal(3, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Must_ValueParam_SatisfiesPredicate_NoProblems()
    {
        // Arrange
        int value = 5;
        int threshold = 3;
        bool Predicate(int v, int t) => v > t;
        string Formatter(string display, int v, int t) => $"{display}: {v}>{t}";

        // Act
        var set = Rules.Set().Must(value, threshold, Predicate, Formatter, "custom.must.param");

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Must_ValueParam_FailsPredicate_ProducesProblem_WithRuleAndCurrent()
    {
        // Arrange
        int value = 2;
        int threshold = 3;
        bool Predicate(int v, int t) => v > t;
        string Formatter(string display, int v, int t) => $"{display}: {v}>{t}";

        // Act
        var set = Rules.Set().Must(value, threshold, Predicate, Formatter, "custom.must.param");

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal("custom.must.param", p.Extensions![Rules.RuleProperty]);
        Assert.Equal(2, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void BothMust_BothSatisfy_NoProblems()
    {
        // Arrange
        int a = 5;
        int b = 10;
        bool Predicate(int x, int y) => x < y;
        string Formatter(string d1, string d2, int x, int y) => $"{d1}<{d2}";

        // Act
        var set = Rules.Set().BothMust(a, b, Predicate, Formatter, "custom.both.must");

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void BothMust_FailsPredicate_ProducesProblem_WithRule_Properties_Values()
    {
        // Arrange
        int a = 10;
        int b = 5;
        bool Predicate(int x, int y) => x < y;
        string Formatter(string d1, string d2, int x, int y) => $"{d1}<{d2}";

        // Act
        var set = Rules.Set().BothMust(a, b, Predicate, Formatter, "custom.both.must");

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal("custom.both.must", p.Extensions![Rules.RuleProperty]);
        var props = Assert.IsType<string?[]>(p.Extensions["properties"]);
        Assert.Equal(new[] { nameof(a), nameof(b) }, props);
        var values = Assert.IsType<object?[]>(p.Extensions["values"]);
        Assert.Equal([10, 5], values);
    }

    [Fact]
    public void BothMust_WithParam_BothSatisfy_NoProblems()
    {
        // Arrange
        string s1 = "abc";
        string s2 = "abcd";
        int min = 3;
        bool Predicate(string x, string y, int m) => x.Length >= m && y.Length >= m;
        string Formatter(string d1, string d2, string x, string y, int m) => $"{d1},{d2}";

        // Act
        var set = Rules.Set().BothMust(s1, s2, min, Predicate, Formatter, "custom.both.must.param");

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void BothMust_WithParam_FailsPredicate_ProducesProblem_WithRule_Properties_Values()
    {
        // Arrange
        string s1 = "ab";
        string s2 = "a";
        int min = 3;
        bool Predicate(string x, string y, int m) => x.Length >= m && y.Length >= m;
        string Formatter(string d1, string d2, string x, string y, int m) => $"{d1},{d2}";

        // Act
        var set = Rules.Set().BothMust(s1, s2, min, Predicate, Formatter, "custom.both.must.param");

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal("custom.both.must.param", p.Extensions![Rules.RuleProperty]);
        var props = Assert.IsType<string?[]>(p.Extensions["properties"]);
        Assert.Equal(new[] { nameof(s1), nameof(s2) }, props);
        var values = Assert.IsType<object?[]>(p.Extensions["values"]);
        Assert.Equal([s1, s2], values);
    }
}
