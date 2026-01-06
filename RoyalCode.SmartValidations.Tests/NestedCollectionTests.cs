namespace RoyalCode.SmartValidations.Tests;

using RoyalCode.SmartProblems;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public class NestedCollectionTests
{
    [Fact]
    public void AllNested_NoProblems()
    {
        // Arrange
        var order = new OrderColl
        {
            Addresses =
            [
                new AddressColl { Street = "123 Main St", City = "Anytown", ZipCode = "12345", Country = "USA" },
                new AddressColl { Street = "456 Oak Ave", City = "Othertown", ZipCode = "67890", Country = "USA" }
            ]
        };

        // Act
        var hasProblems = order.HasProblems(out var problems);

        // Assert
        Assert.False(hasProblems);
        Assert.Null(problems);
    }

    [Fact]
    public void AllNested_WithNestedProblems()
    {
        // Arrange
        var order = new OrderColl
        {
            Addresses =
            [
                new AddressColl { Street = string.Empty, City = "Anytown", ZipCode = "12345", Country = "USA" },
                new AddressColl { Street = "456 Oak Ave", City = "Othertown", ZipCode = "67890", Country = "USA" }
            ]
        };

        // Act
        var hasProblems = order.HasProblems(out var problems);

        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Single(problems);
    }

    [Fact]
    public void AllNested_WithNestedProblems_And_NestedValidable()
    {
        // Arrange
        var foo = new FooColl
        {
            // FooColl.Value empty -> 1 problem
            Bars =
            [
                new BarColl { Value = string.Empty }, // -> 1 problem
                new BarColl { Value = "ok" }
            ]
        };

        // Act
        var hasProblems = foo.HasProblems(out var problems);

        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Equal(2, problems.Count);
    }

    [Fact]
    public void AllNested_WithNestedProblems_And_NestedValidable_And_NestedValidateFunc()
    {
        // Arrange
        var foo = new FooColl
        {
            Value = "Foo Value", // valid
            Bars = [ new BarColl { Value = "Bar Value" } ], // valid
            Bazes = [ new BazColl() ] // invalid -> 1 problem
        };

        // Act
        var hasProblems = foo.HasProblems(out var problems);

        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Single(problems);
    }

    [Fact]
    public void AllNested_NullCollection_NoProblems()
    {
        // Arrange
        var order = new OrderColl { Addresses = null };

        // Act
        var hasProblems = order.HasProblems(out var problems);

        // Assert
        Assert.False(hasProblems);
        Assert.Null(problems);
    }
}

[DisplayName("Order (Collection)")]
file class OrderColl : IValidable
{
    [DisplayName("Addresses")]
    public List<AddressColl>? Addresses { get; set; }

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<OrderColl>()
            .AllNested(Addresses, address => Rules.Set<AddressColl>()
                .WithPropertyPrefix("address")
                .NotEmpty(address.Street)
                .NotEmpty(address.City)
                .NotEmpty(address.ZipCode)
                .NotEmpty(address.Country))
            .HasProblems(out problems);
    }
}

[DisplayName("Address (Collection)")]
file class AddressColl
{
    [DisplayName("Street")]
    public string Street { get; set; } = string.Empty;

    [DisplayName("City")]
    public string City { get; set; } = string.Empty;

    [DisplayName("Zip Code")]
    public string ZipCode { get; set; } = string.Empty;

    [DisplayName("Country")]
    public string Country { get; set; } = string.Empty;
}

file class FooColl : IValidable
{
    public string? Value { get; set; } = string.Empty;

    public IEnumerable<BarColl>? Bars { get; set; }

    public IEnumerable<BazColl>? Bazes { get; set; }

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<FooColl>()
            .NotEmpty(Value)
            .AllNested(Bars)
            .AllNested(Bazes, b => b.HasProblems)
            .HasProblems(out problems);
    }
}

file class BarColl : IValidable
{
    public string? Value { get; set; } = string.Empty;

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<BarColl>()
            .NotEmpty(Value)
            .HasProblems(out problems);
    }
}

file class BazColl
{
    public string? Value { get; set; } = string.Empty;

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<BazColl>()
            .NotEmpty(Value)
            .HasProblems(out problems);
    }
}
