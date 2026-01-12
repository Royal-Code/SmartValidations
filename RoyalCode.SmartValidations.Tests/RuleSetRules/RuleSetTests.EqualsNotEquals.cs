namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void Equal_String_Mismatch_ProducesProblem()
    {
        // Arrange
        string? value = "abc";

        // Act
        var set = Rules.Set<EqualsModel>().Equal(value, "xyz", StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Equals, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("abc", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal("xyz", p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void Equal_String_Match_NoProblems()
    {
        // Arrange
        string? value = "abc";

        // Act
        var set = Rules.Set().Equal(value, "abc", StringComparison.Ordinal);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Equal_Generic_Mismatch_ProducesProblem()
    {
        // Arrange
        int? value = 1;

        // Act
        var set = Rules.Set<EqualsModel>().Equal(value, 2);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Equals, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(1, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(2, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void NotEqual_String_Match_ProducesProblem()
    {
        // Arrange
        string? value = "same";

        // Act
        var set = Rules.Set<EqualsModel>().NotEqual(value, "same", StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.NotEquals, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("same", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal("same", p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void NotEqual_String_Different_NoProblems()
    {
        // Arrange
        string? value = "a";

        // Act
        var set = Rules.Set().NotEqual(value, "b", StringComparison.Ordinal);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NotEqual_Generic_Match_ProducesProblem()
    {
        // Arrange
        int? value = 10;

        // Act
        var set = Rules.Set<EqualsModel>().NotEqual(value, 10);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.NotEquals, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(10, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(10, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void BothEqual_Strings_Different_ProducesProblem()
    {
        // Arrange
        string? a = "x";
        string? b = "y";

        // Act
        var set = Rules.Set<EqualsPair>().BothEqual(a, b, StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.BothEqual, p.Extensions![Rules.RuleProperty]);
        Assert.True(p.Extensions.TryGetValue("properties", out var propsObj));
        var props = Assert.IsType<string?[]>(propsObj);
        Assert.Equal(new[] { nameof(a), nameof(b) }, props);
        Assert.True(p.Extensions.TryGetValue("values", out var valsObj));
        var vals = Assert.IsType<string[]>(valsObj);
        Assert.Equal(new[] { "x", "y" }, vals);
    }

    [Fact]
    public void BothEqual_Generic_Different_ProducesProblem()
    {
        // Arrange
        int? a = 1;
        int? b = 2;

        // Act
        var set = Rules.Set<EqualsPair>().BothEqual(a, b);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.BothEqual, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void BothNotEqual_Strings_Same_ProducesProblem()
    {
        // Arrange
        string? a = "x";
        string? b = "x";

        // Act
        var set = Rules.Set<EqualsPair>().BothNotEqual(a, b, StringComparison.Ordinal);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.BothNotEqual, p.Extensions![Rules.RuleProperty]);
        Assert.True(p.Extensions.TryGetValue("properties", out var propsObj));
        var props = Assert.IsType<string?[]>(propsObj);
        Assert.Equal(new[] { nameof(a), nameof(b) }, props);
    }
}

file class EqualsModel
{
    [System.ComponentModel.DisplayName("Value Label")]
    public string? Value { get; set; }
}

file class EqualsPair
{
    [System.ComponentModel.DisplayName("A Label")]
    public string? A { get; set; }

    [System.ComponentModel.DisplayName("B Label")]
    public string? B { get; set; }
}
 
