namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void NullOrNotEmpty_String_Null_NoProblems()
    {
        // Arrange
        string? value = null;

        // Act
        var set = Rules.Set().NullOrNotEmpty(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrNotEmpty_String_Empty_ProducesProblem()
    {
        // Arrange
        string? value = "";

        // Act
        var set = Rules.Set<NullOrNotSample>().NullOrNotEmpty(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(value), problem.Property);
        Assert.Equal(Rules.NullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NullOrNotEmpty_Number_NonZero_NoProblems()
    {
        // Arrange
        int value = 1;

        // Act
        var set = Rules.Set().NullOrNotEmpty(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrNotEmpty_Number_Zero_ProducesProblem()
    {
        // Arrange
        int value = 0;

        // Act
        var set = Rules.Set<NullOrNotSample>().NullOrNotEmpty(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(value), problem.Property);
        Assert.Equal(Rules.NullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NullOrNotEmpty_NullableNumber_Null_NoProblems()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set().NullOrNotEmpty(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NullOrNotEmpty_NullableNumber_Zero_ProducesProblem_WithPrefix()
    {
        // Arrange
        int? amount = 0;
        var set = Rules.Set<NullOrNotAmount>().WithPropertyPrefix("req");

        // Act
        set = set.NullOrNotEmpty(amount);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(amount), problem.Property);
        Assert.Equal(Rules.NullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

}

file class NullOrNotSample
{
    [System.ComponentModel.DisplayName("Name Label")]
    public string? Name { get; set; }
}

file class NullOrNotAmount
{
    [System.ComponentModel.DisplayName("Amount Label")]
    public int? Amount { get; set; }
}

 
