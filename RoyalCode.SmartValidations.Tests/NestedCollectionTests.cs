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

    [Fact]
    public void Nested_NullItem_IsSkipped()
    {
        // Arrange
        List<BarColl> bars = [null!, new BarColl { Value = "ok" }];

        // Act
        var set = Rules.Set().Nested(bars);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Nested_Func_NullItem_IsSkipped()
    {
        // Arrange
        List<AddressColl> addresses =
        [
            null!,
            new AddressColl { Street = "456 Oak Ave", City = "Othertown", ZipCode = "67890", Country = "USA" }
        ];

        // Act
        var set = Rules.Set().Nested(addresses, address => Rules.Set<AddressColl>()
            .WithPropertyPrefix("address")
            .NotEmpty(address.Street)
            .NotEmpty(address.City)
            .NotEmpty(address.ZipCode)
            .NotEmpty(address.Country));

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void NotNullNested_Func_NullItem_ReportsProblem()
    {
        // Arrange
        List<AddressColl> addresses =
        [
            null!,
            new AddressColl { Street = "456 Oak Ave", City = "Othertown", ZipCode = "67890", Country = "USA" }
        ];

        // Act
        var set = Rules.Set().NotNullNested(addresses, address => Rules.Set<AddressColl>()
            .WithPropertyPrefix("address")
            .NotEmpty(address.Street)
            .NotEmpty(address.City)
            .NotEmpty(address.ZipCode)
            .NotEmpty(address.Country));

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal($"{nameof(addresses)}[0]", p.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotNullNested_IValidable_NullItem_ReportsProblem()
    {
        // Arrange
        List<BarColl> bars = [null!, new BarColl { Value = "ok" }];

        // Act
        var set = Rules.Set().NotNullNested(bars);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal($"{nameof(bars)}[0]", p.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotNullNested_ValidateFunc_NullItem_ReportsProblem()
    {
        // Arrange
        List<BazColl> bazes = [null!, new BazColl { Value = "ok" }];

        // Act
        var set = Rules.Set().NotNullNested(bazes, b => b.HasProblems);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal($"{nameof(bazes)}[0]", p.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void NotNullNested_NullItemAndInvalidItem_ReportsBothProblems()
    {
        // Arrange
        List<BarColl> bars = [null!, new BarColl { Value = string.Empty }];

        // Act
        var set = Rules.Set().NotNullNested(bars);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        Assert.Equal(2, problems!.Count);
        Assert.Contains(problems, p => p.Property == $"{nameof(bars)}[0]");
        Assert.Contains(problems, p => p.Property == $"{nameof(bars)}[1].{nameof(BarColl.Value)}");
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
            .Nested(Addresses, address => Rules.Set<AddressColl>()
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
            .Nested(Bars)
            .Nested(Bazes, b => b.HasProblems)
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
