namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void LessThan_Int_Less_NoProblems()
    {
        // Arrange
        int value1 = 2;
        int value2 = 3;

        // Act
        var set = Rules.Set().LessThan(value1, value2);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void LessThan_Int_NotLess_ProducesProblem()
    {
        // Arrange
        int value1 = 3;
        int value2 = 3;

        // Act
        var set = Rules.Set().LessThan(value1, value2);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.LessThan, p.Extensions![Rules.RuleProperty]);
        Assert.True(p.Extensions.TryGetValue("properties", out var propsObj));
        var props = Assert.IsType<string?[]>(propsObj);
        Assert.Equal(new[] { nameof(value1), nameof(value2) }, props);
        var vals = Assert.IsType<int[]>(p.Extensions["values"]);
        Assert.Equal([3, 3], vals);
    }

    [Fact]
    public void LessThan_NullableInt_Null_ProducesProblem()
    {
        // Arrange
        int? value1 = 1;
        int? value2 = null;

        // Act
        var set = Rules.Set().LessThan(value1, value2);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.LessThan, p.Extensions![Rules.RuleProperty]);
        var props = Assert.IsType<string?[]>(p.Extensions["properties"]);
        Assert.Equal(new[] { nameof(value1), nameof(value2) }, props);
        var vals = Assert.IsType<int?[]>(p.Extensions["values"]);
        Assert.Equal([1, null], vals);
    }

    [Fact]
    public void LessThanOrEqual_Int_Equal_NoProblems()
    {
        // Arrange
        int value1 = 5;
        int value2 = 5;

        // Act
        var set = Rules.Set().LessThanOrEqual(value1, value2);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void LessThanOrEqual_Int_Greater_ProducesProblem()
    {
        // Arrange
        int value1 = 6;
        int value2 = 5;

        // Act
        var set = Rules.Set().LessThanOrEqual(value1, value2);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.LessThanOrEqual, p.Extensions[Rules.RuleProperty]);
        var props = Assert.IsType<string?[]>(p.Extensions["properties"]);
        Assert.Equal(new[] { nameof(value1), nameof(value2) }, props);
        var vals = Assert.IsType<int[]>(p.Extensions["values"]);
        Assert.Equal(new[] { 6, 5 }, vals);
    }

    [Fact]
    public void GreaterThan_Int_Greater_NoProblems()
    {
        // Arrange
        int value1 = 10;
        int value2 = 7;

        // Act
        var set = Rules.Set().GreaterThan(value1, value2);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void GreaterThan_Int_NotGreater_ProducesProblem()
    {
        // Arrange
        int value1 = 7;
        int value2 = 7;

        // Act
        var set = Rules.Set().GreaterThan(value1, value2);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.GreaterThan, p.Extensions[Rules.RuleProperty]);
        var props = Assert.IsType<string?[]>(p.Extensions["properties"]);
        Assert.Equal(new[] { nameof(value1), nameof(value2) }, props);
        var vals = Assert.IsType<int[]>(p.Extensions["values"]);
        Assert.Equal(new[] { 7, 7 }, vals);
    }

    [Fact]
    public void GreaterThanOrEqual_Int_Equal_NoProblems()
    {
        // Arrange
        int value1 = 8;
        int value2 = 8;

        // Act
        var set = Rules.Set().GreaterThanOrEqual(value1, value2);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void GreaterThanOrEqual_Int_Less_ProducesProblem()
    {
        // Arrange
        int value1 = 4;
        int value2 = 5;

        // Act
        var set = Rules.Set().GreaterThanOrEqual(value1, value2);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.GreaterThanOrEqual, p.Extensions[Rules.RuleProperty]);
        var props = Assert.IsType<string?[]>(p.Extensions["properties"]);
        Assert.Equal(new[] { nameof(value1), nameof(value2) }, props);
        var vals = Assert.IsType<int[]>(p.Extensions["values"]);
        Assert.Equal(new[] { 4, 5 }, vals);
    }
}
