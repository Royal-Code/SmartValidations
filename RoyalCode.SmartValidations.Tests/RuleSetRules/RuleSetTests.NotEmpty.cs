namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void NotEmpty_String_Filled_NoProblems()
    {
        // Arrange
        string? value = "ok";

        // Act
        var set = Rules.Set().NotEmpty(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NotEmpty_String_Empty_ProducesProblem()
    {
        // Arrange
        string? value = "";

        // Act
        var set = Rules.Set<NotEmptySample>().NotEmpty(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(value), problem.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotEmpty_Number_NonZero_NoProblems()
    {
        // Arrange
        int value = 1;

        // Act
        var set = Rules.Set().NotEmpty(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NotEmpty_Number_Zero_ProducesProblem()
    {
        // Arrange
        int value = 0;

        // Act
        var set = Rules.Set<NotEmptySample>().NotEmpty(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(value), problem.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotEmpty_NullableNumber_Null_ProducesProblem()
    {
        // Arrange
        int? value = null;

        // Act
        var set = Rules.Set<NotEmptyAmount>().NotEmpty(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(value), problem.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotEmpty_Array_Empty_ProducesProblem()
    {
        // Arrange
        int[]? arr = Array.Empty<int>();

        // Act
        var set = Rules.Set<NotEmptySample>().NotEmpty(arr);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(arr), problem.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotEmpty_Collection_Empty_ProducesProblem()
    {
        // Arrange
        ICollection<string>? list = new List<string>();

        // Act
        var set = Rules.Set<NotEmptySample>().NotEmpty(list);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(list), problem.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotEmpty_DateTime_Default_ProducesProblem()
    {
        // Arrange
        DateTime value = default;

        // Act
        var set = Rules.Set<NotEmptySample>().NotEmpty(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(value), problem.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void BothNullOrNotEmpty_OneFilledOtherEmpty_ProducesProblem()
    {
        // Arrange
        string? a = "x";
        string? b = "";

        // Act
        var set = Rules.Set<NotEmptyPair>().BothNullOrNotEmpty(a, b);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(Rules.BothNullOrNot, problem.Extensions![Rules.RuleProperty]);
        Assert.True(problem.Extensions.TryGetValue("properties", out var propsObj));
        var props = Assert.IsType<string?[]>(propsObj);
        Assert.Equal(new[] { nameof(a), nameof(b) }, props);
    }
}

file class NotEmptySample
{
    [System.ComponentModel.DisplayName("Name Label")]
    public string? Name { get; set; }
}

file class NotEmptyAmount
{
    [System.ComponentModel.DisplayName("Amount Label")]
    public int? Amount { get; set; }
}

file class NotEmptyPair
{
    [System.ComponentModel.DisplayName("A Label")]
    public string? A { get; set; }

    [System.ComponentModel.DisplayName("B Label")]
    public string? B { get; set; }
}
