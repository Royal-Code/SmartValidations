using RoyalCode.SmartProblems;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void NotNullNested_ValueNull_ProducesProblem()
    {
        // Arrange
        Child? child = null;

        // Act
        var set = Rules.Set().NotNullNested(child);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        Assert.Equal(nameof(child), problem.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, problem.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void Nested_WithFunction_ChainsProperty_WithoutPrefix()
    {
        // Arrange
        var parent = new Parent { Child = new Child { IsValid = false } };
        Problems? ValidateChild(Child c)
        {
            if (c.IsValid) 
                return null;
            
            return Problems.InvalidParameter("invalid", "ChildProp");
        }

        // Act
        var set = Rules.Set<Parent>()
            .Nested(parent.Child, ValidateChild);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems);
        Assert.Equal("parent.Child.ChildProp", problem.Property);
    }

    [Fact]
    public void Nested_WithFunction_ChainsProperty_WithPrefix()
    {
        // Arrange
        var parent = new Parent { Child = new Child { IsValid = false } };
        Problems? ValidateChild(Child c)
        {
            if (c.IsValid) 
                return null;

            return Problems.InvalidParameter("invalid", "ChildProp");
        }
        var set = Rules.Set<Parent>().WithPropertyPrefix("parent");

        // Act
        set = set.Nested(parent.Child, ValidateChild);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        // Prefix removed; property should not start with the prefix
        Assert.Equal("Child.ChildProp", problem.Property);
    }

    [Fact]
    public void Nested_WithFunction_ChainsProperty_WithPrefix_TwoTimes()
    {
        // Arrange
        var parent = new Parent { Child = new Child { IsValid = false } };
        Problems? ValidateChild(Child c)
        {
            if (c.IsValid)
                return null;

            return Rules.Set<Child>()
                .WithPropertyPrefix("c")
                .Equal(c.IsValid, true);
        }
        var set = Rules.Set<Parent>().WithPropertyPrefix("parent");

        // Act
        set = set.Nested(parent.Child, ValidateChild);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var problem = Assert.Single(problems!);
        // Prefix removed; property should not start with the prefix
        Assert.Equal("Child.IsValid", problem.Property);
    }
}

file class Parent
{
    public Child? Child { get; set; }
}

file class Child : IValidable
{
    public bool IsValid { get; set; } = true;

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        problems = null!;
        if (IsValid)
            return false;

        problems = Problems.InvalidParameter("invalid", "ChildProp");
        return true;
    }
}

