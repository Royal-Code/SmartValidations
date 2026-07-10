namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void Positive_Int_Positive_NoProblems()
    {
        // Arrange
        int value = 1;

        // Act
        var set = Rules.Set().Positive(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Positive_Int_Zero_ProducesProblem()
    {
        // Arrange
        int value = 0;

        // Act
        var set = Rules.Set().Positive(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Positive, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Positive_NullableInt_Null_ProducesProblem()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().Positive(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Positive, p.Extensions![Rules.RuleProperty]);
        Assert.Null(p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Negative_Int_Negative_NoProblems()
    {
        // Arrange
        int value = -1;

        // Act
        var set = Rules.Set().Negative(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Negative_Int_Positive_ProducesProblem()
    {
        // Arrange
        int value = 1;

        // Act
        var set = Rules.Set().Negative(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Negative, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Zero_Int_Zero_NoProblems()
    {
        // Arrange
        int value = 0;

        // Act
        var set = Rules.Set().Zero(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Zero_Int_NonZero_ProducesProblem()
    {
        // Arrange
        int value = 1;

        // Act
        var set = Rules.Set().Zero(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.Zero, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void NotZero_Int_NonZero_NoProblems()
    {
        // Arrange
        int value = 1;

        // Act
        var set = Rules.Set().NotZero(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NotZero_Int_Zero_ProducesProblem()
    {
        // Arrange
        int value = 0;

        // Act
        var set = Rules.Set().NotZero(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(value), p.Property);
        Assert.Equal(Rules.NotZero, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(value, p.Extensions[Rules.CurrentValueProperty]);
    }
}
