using RoyalCode.SmartProblems;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void Validate_Struct_Valid_NoProblems()
    {
        // Arrange
        ValidStruct value = new(id: 1, name: "John", age: 30);

        // Act
        var set = Rules.Set().Validate(value);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Validate_Struct_Invalid_ReplacesPropertyName()
    {
        // Arrange
        ValidStruct value = new(id: 0, name: "", age: 10);

        // Act
        var set = Rules.Set().Validate(value);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        Assert.Equal(3, problems.Count);
        Assert.All(problems!, p => Assert.Equal(nameof(value), p.Property));
    }

    [Fact]
    public void Validate_Struct_WithPropertyPrefix_RemovesPrefix()
    {
        // Arrange
        var model = new { Item = new ValidStruct(id: 0, name: "", age: 10) };

        // Act
        var set = Rules.Set<object>()
            .WithPropertyPrefix("model")
            .Validate(model.Item);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        Assert.Equal(3, problems.Count);
        Assert.All(problems, p => Assert.Equal("Item", p.Property));
    }

    [Fact]
    public void Validate_Enumerable_ReplacesPropertyWithCollectionName()
    {
        // Arrange
        var items = new[]
        {
            new ValidStruct(id: 0, name: "", age: 10),
            new ValidStruct(id: 1, name: "Ok", age: 20),
            new ValidStruct(id: -5, name: null, age: 1)
        };

        // Act
        var set = Rules.Set().Validate(items);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        // 1st item: 3 problems; 2nd item: 0; 3rd item: 3 problems => total 6
        Assert.Equal(6, problems.Count);

        var regex = new System.Text.RegularExpressions.Regex(@"^items\[\d+\]$");
        Assert.All(problems, p => Assert.Matches(regex, p.Property));
    }
}

file readonly struct ValidStruct : IValidable
{
    public int Id { get; }
    public string? Name { get; }
    public int Age { get; }

    public ValidStruct(int id, string? name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        Problems result = [];

        if (Id <= 0)
            result.Add(Problems.InvalidParameter("invalid id", "Id"));

        if (string.IsNullOrWhiteSpace(Name))
            result.Add(Problems.InvalidParameter("invalid name", "Name"));

        if (Age < 18)
            result.Add(Problems.InvalidParameter("invalid age", "Age"));

        if (result.Count == 0)
        {
            problems = null!;
            return false;
        }

        problems = result;
        return true;
    }
}

