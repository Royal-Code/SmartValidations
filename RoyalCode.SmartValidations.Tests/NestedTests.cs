using RoyalCode.SmartProblems;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace RoyalCode.SmartValidations.Tests;

public class NestedTests
{
    [Fact]
    public void NestedProperties_NoProblems()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerName = "John Doe",
            TotalAmount = 100.50m,
            ShippingAddress = new Address
            {
                Street = "123 Main St",
                City = "Anytown",
                ZipCode = "12345",
                Country = "USA"
            }
        };

        // Act
        var hasProblems = order.HasProblems(out var problems);

        // Assert
        Assert.False(hasProblems);
        Assert.Null(problems);
    }

    [Fact]
    public void NestedProperties_WithNestedProblems()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerName = "John Doe",
            TotalAmount = -50.00m, // Invalid total amount
            ShippingAddress = new Address
            {
                Street = "", // Empty street
                City = "Anytown",
                ZipCode = "12345",
                Country = "USA"
            }
        };

        // Act
        var hasProblems = order.HasProblems(out var problems);

        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Equal(2, problems.Count); // Expecting problems for TotalAmount and ShippingAddress.Street
    }

    [Fact]
    public void NestedProperties_WithNestedProblems_And_NestedValidable()
    {
        // Arrange
        var foo = new Foo
        {
            Bar = new Bar(),
            Baz = new Baz
            {
                Value = "Baz Value" // This is valid
            }
        };

        // Act
        var hasProblems = foo.HasProblems(out var problems);

        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Equal(2, problems.Count); // Foo.Value and Bar.Value should be empty
    }

    [Fact]
    public void NestedProperties_WithNestedProblems_And_NestedValidable_And_NestedValidateFunc()
    {
        // Arrange
        var foo = new Foo
        {
            Value = "Foo Value", // This is valid
            Bar = new Bar
            {
                Value = "Bar Value" // This is valid
            },
            Baz = new Baz() // This will have problems
        };
        // Act
        var hasProblems = foo.HasProblems(out var problems);
        // Assert
        Assert.True(hasProblems);
        Assert.NotNull(problems);
        Assert.Single(problems);
    }
}

[DisplayName("Order")]
file class Order : IValidable
{
    [DisplayName("Order Id")]
    public int Id { get; set; }

    [DisplayName("Customer Name")]
    public string CustomerName { get; set; } = string.Empty;

    [DisplayName("Total Amount")]
    public decimal TotalAmount { get; set; }

    [DisplayName("Shipping Address")]
    public Address? ShippingAddress { get; set; }

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<Order>()
            .NotNull(this)
            .NotEmpty(CustomerName)
            .GreaterThan(TotalAmount, 0)
            .NotNullNested(ShippingAddress, address => Rules.Set<Address>()
                .WithPropertyPrefix("address")
                .NotEmpty(address.Street)
                .NotEmpty(address.City)
                .NotEmpty(address.ZipCode)
                .NotEmpty(address.Country))
            .HasProblems(out problems);
    }
}

[DisplayName("Address")]
file class Address
{
    [DisplayName("'Street'")]
    public string Street { get; set; } = string.Empty;

    [DisplayName("City")]
    public string City { get; set; } = string.Empty;

    [DisplayName("Zip Code")]
    public string ZipCode { get; set; } = string.Empty;

    [DisplayName("Country")]
    public string Country { get; set; } = string.Empty;
}

file class Foo : IValidable
{
    public string? Value { get; set; } = string.Empty;

    public Bar? Bar { get; set; }

    public Baz? Baz { get; set; }

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<Foo>()
            .NotEmpty(Value)
            .NotNullNested(Bar)
            .Nested(Baz, b => b.HasProblems)
            .HasProblems(out problems);
    }
}

file class Bar : IValidable
{
    public string? Value { get; set; } = string.Empty;

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<Bar>()
            .NotEmpty(Value)
            .HasProblems(out problems);
    }
}

file class Baz
{
    public string? Value { get; set; } = string.Empty;

    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        return Rules.Set<Baz>()
            .NotEmpty(Value)
            .HasProblems(out problems);
    }
}