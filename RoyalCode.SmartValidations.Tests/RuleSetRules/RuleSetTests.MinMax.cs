namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void Min_Int_LessThan_ProducesProblem()
    {
        // Arrange
        int value = 4;

        // Act
        var set = Rules.Set().Min(value, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Min, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(4, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(5, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void Min_Int_GreaterOrEqual_NoProblems()
    {
        // Arrange
        int value = 5;

        // Act
        var set = Rules.Set().Min(value, 5);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Min_NullableInt_Null_ProducesProblem()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().Min(value, 1);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Min, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NullOrMin_Null_NoProblems()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().NullOrMin(value, 10);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrMin_LessThan_ProducesProblem()
    {
        // Arrange
        int? value = 3;

        // Act
        var set = Rules.Set().NullOrMin(value, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NullOrMin, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(3, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(5, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void MinLength_String_TooShort_ProducesProblem()
    {
        // Arrange
        string? value = "abc";

        // Act
        var set = Rules.Set().MinLength(value, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.MinLength, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("abc", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal("5", p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void MinLength_String_LongEnough_NoProblems()
    {
        // Arrange
        string? value = "abcde";

        // Act
        var set = Rules.Set().MinLength(value, 5);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrMinLength_Null_NoProblems()
    {
        // Arrange
        string? value = null;

        // Act
        var set = Rules.Set().NullOrMinLength(value, 5);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrMinLength_TooShort_ProducesProblem()
    {
        // Arrange
        string? value = "abc";

        // Act
        var set = Rules.Set().NullOrMinLength(value, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NullOrMinLength, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("abc", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal("5", p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void Max_Int_GreaterThan_ProducesProblem()
    {
        // Arrange
        int value = 10;

        // Act
        var set = Rules.Set().Max(value, 9);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Max, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(10, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(9, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void Max_Int_LessOrEqual_NoProblems()
    {
        // Arrange
        int value = 9;

        // Act
        var set = Rules.Set().Max(value, 10);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Max_NullableInt_Null_ProducesProblem()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().Max(value, 10);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Max, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NullOrMax_Null_NoProblems()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().NullOrMax(value, 10);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrMax_GreaterThan_ProducesProblem()
    {
        // Arrange
        int? value = 11;

        // Act
        var set = Rules.Set().NullOrMax(value, 10);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NullOrMax, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(11, p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal(10, p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void MaxLength_String_TooLong_ProducesProblem()
    {
        // Arrange
        string? value = "abcdef";

        // Act
        var set = Rules.Set().MaxLength(value, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.MaxLength, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("abcdef", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal("5", p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void MaxLength_String_ShortEnough_NoProblems()
    {
        // Arrange
        string? value = "abc";

        // Act
        var set = Rules.Set().MaxLength(value, 5);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrMaxLength_Null_NoProblems()
    {
        // Arrange
        string? value = null;

        // Act
        var set = Rules.Set().NullOrMaxLength(value, 5);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrMaxLength_TooLong_ProducesProblem()
    {
        // Arrange
        string? value = "abcdef";

        // Act
        var set = Rules.Set().NullOrMaxLength(value, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NullOrMaxLength, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("abcdef", p.Extensions[Rules.CurrentValueProperty]);
        Assert.Equal("5", p.Extensions[Rules.ExpectedValueProperty]);
    }

    [Fact]
    public void MinMax_Int_OutOfRange_ProducesProblem()
    {
        // Arrange
        int value = 1;

        // Act
        var set = Rules.Set().MinMax(value, 2, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.MinMax, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(1, p.Extensions[Rules.CurrentValueProperty]);
        var expected = Assert.IsType<int[]>(p.Extensions[Rules.ExpectedValueProperty]);
        Assert.Equal([2, 5], expected);
    }

    [Fact]
    public void MinMax_Int_InRange_NoProblems()
    {
        // Arrange
        int value = 3;

        // Act
        var set = Rules.Set().MinMax(value, 2, 5);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void MinMax_NullableInt_Null_ProducesProblem()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().MinMax(value, 2, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.MinMax, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NullOrMinMax_Null_NoProblems()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().NullOrMinMax(value, 2, 5);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrMinMax_OutOfRange_ProducesProblem()
    {
        // Arrange
        int? value = 10;

        // Act
        var set = Rules.Set().NullOrMinMax(value, 2, 5);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NullOrMinMax, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(10, p.Extensions[Rules.CurrentValueProperty]);
        var expected = Assert.IsType<int?[]>(p.Extensions[Rules.ExpectedValueProperty]);
        Assert.Equal([2, 5], expected);
    }

    [Fact]
    public void Length_String_OutOfRange_ProducesProblem()
    {
        // Arrange
        string? value = "a";

        // Act
        var set = Rules.Set().Length(value, 2, 4);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Length, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("a", p.Extensions[Rules.CurrentValueProperty]);
        var expected = Assert.IsType<string[]>(p.Extensions[Rules.ExpectedValueProperty]);
        Assert.Equal(new[] { "2", "4" }, expected);
    }

    [Fact]
    public void Length_String_InRange_NoProblems()
    {
        // Arrange
        string? value = "abcd";

        // Act
        var set = Rules.Set().Length(value, 2, 4);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrLength_Null_NoProblems()
    {
        // Arrange
        string? value = null;

        // Act
        var set = Rules.Set().NullOrLength(value, 2, 4);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrLength_OutOfRange_ProducesProblem()
    {
        // Arrange
        string? value = "a";

        // Act
        var set = Rules.Set().NullOrLength(value, 2, 4);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(Rules.NullOrLength, p.Extensions![Rules.RuleProperty]);
        Assert.Equal("a", p.Extensions[Rules.CurrentValueProperty]);
        var expected = Assert.IsType<string[]>(p.Extensions[Rules.ExpectedValueProperty]);
        Assert.Equal(new[] { "2", "4" }, expected);
    }
}
