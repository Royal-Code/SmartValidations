using RoyalCode.SmartProblems;

namespace RoyalCode.SmartValidations.Tests;

public class ValidateTests
{
    [Fact]
    public void Validate_Struct_NoProblems()
    {
        // Arrange
        var product = new Product
        {
            Name = "Valid",
            Quantity = 1,
            Price = 0.01m
        };

        // Act
        var rules = Rules.Set().Validate(product);
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.False(hasProblems);
        Assert.Null(problems);
    }

    [Fact]
    public void Validate_Struct_WithProblems()
    {
        // Arrange
        var product = new Product(); // default values cause problems

        // Act
        var rules = Rules.Set().Validate(product);
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Equal(3, problems.Count); // Name, Quantity, Price
    }

    [Fact]
    public void Validate_StructCollection_AllValid_NoProblems()
    {
        // Arrange
        var products = new[]
        {
            new Product { Name = "A", Quantity = 1, Price = 0.01m },
            new Product { Name = "B", Quantity = 2, Price = 10.0m }
        };

        // Act
        var rules = Rules.Set().Validate(products);
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.False(hasProblems);
        Assert.Null(problems);
    }

    [Fact]
    public void Validate_StructCollection_WithProblems()
    {
        // Arrange
        var products = new[]
        {
            new Product(), // invalid -> 3 problems
            new Product { Name = "Valid", Quantity = 1, Price = 0.01m } // valid
        };

        // Act
        var rules = Rules.Set().Validate(products);
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Equal(3, problems.Count);
    }

    [Fact]
    public void Validate_StructCollection_Null_NoProblems()
    {
        // Arrange
        Product[]? products = null;

        // Act
        var rules = Rules.Set().Validate(products!);
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.False(hasProblems);
        Assert.Null(problems);
    }
}

file struct Product : IValidable
{
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public bool HasProblems(out Problems? problems)
    {
        return Rules.Set<Product>()
            .NotEmpty(Name)
            .Min(Quantity, 1)
            .Min(Price, 0.01m)
            .HasProblems(out problems);
    }
}
