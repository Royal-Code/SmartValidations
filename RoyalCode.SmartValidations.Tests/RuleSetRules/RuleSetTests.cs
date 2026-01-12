using RoyalCode.SmartProblems;
using System.ComponentModel;

namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void ImplicitOperator_NoProblems_ReturnsNull()
    {
        // Arrange
        var set = Rules.Set();

        // Act
        Problems? problems = set;

        // Assert
        Assert.Null(problems);
    }

    [Fact]
    public void ImplicitOperator_WithProblem_ReturnsProblems()
    {
        // Arrange
        var set = Rules.Set()
            .WithProblem(Problems.InvalidParameter("error", "prop"));

        // Act
        Problems? problems = set;

        // Assert
        Assert.NotNull(problems);
        Assert.Single(problems);
    }

    [Fact]
    public void For_UsesModelTypeForDisplayName()
    {
        // Arrange
        var set = Rules.Set<SampleModel>();

        // Act
        set.GetPropertyNameAndDisplayName(nameof(SampleModel.Name), out var propertyName, out var displayName);

        // Assert
        Assert.Equal(nameof(SampleModel.Name), propertyName);
        Assert.Equal("Name Label", displayName);
    }

    [Fact]
    public void DefaultConstructor_HasNoProblems_And_ProvidesPropertyInfo()
    {
        // Arrange
        var set = Rules.Set(); // default RuleSet

        // Act
        var hasProblems = set.HasProblems(out var problems);
        set.GetPropertyNameAndDisplayName("Any.Property", out var propertyName, out var displayName);

        // Assert
        Assert.False(hasProblems);
        Assert.Null(problems);
        Assert.Equal("Any.Property", propertyName);
        Assert.Equal("Any.Property", displayName);
    }

    [Fact]
    public void WithProblem_AddsProblem()
    {
        // Arrange
        var set = Rules.Set();

        // Act
        set = set.WithProblem(Problems.InvalidParameter("Invalid", "Field"));

        // Assert
        Assert.True(set.HasProblems(out var problems));
        Assert.NotNull(problems);
        Assert.Single(problems!);
        Assert.Equal("Field", problems![0].Property);
    }

    [Fact]
    public void WithPropertyPrefix_RemovesPrefixFromPropertyName()
    {
        // Arrange
        var set = Rules.Set<SampleAddress>().WithPropertyPrefix("addr");

        // Act
        set.GetPropertyNameAndDisplayName("addr.Street", out var propertyName, out var displayName);

        // Assert
        Assert.Equal("Street", propertyName);
        Assert.Equal("Street Label", displayName);
    }

    [Fact]
    public void HasProblems_ReturnsExpected()
    {
        // Arrange
        var ok = Rules.Set();
        var bad = Rules.Set().WithProblem(Problems.InvalidParameter("x", "p"));

        // Act
        var okHas = ok.HasProblems(out var okProblems);
        var badHas = bad.HasProblems(out var badProblems);

        // Assert
        Assert.False(okHas);
        Assert.Null(okProblems);
        Assert.True(badHas);
        Assert.NotNull(badProblems);
        Assert.Single(badProblems);
    }

    [Fact]
    public void GetPropertyNameAndDisplayName_NoPrefix_ResolvesDisplayName()
    {
        // Arrange
        var set = Rules.Set<SampleAddress>();

        // Act
        set.GetPropertyNameAndDisplayName(nameof(SampleAddress.ZipCode), out var propertyName, out var displayName);

        // Assert
        Assert.Equal(nameof(SampleAddress.ZipCode), propertyName);
        Assert.Equal("Zip Code Label", displayName);
    }
}

// Helpers for display name resolution in tests

file class SampleModel
{
    [DisplayName("Name Label")]
    public string Name { get; set; } = string.Empty;
}

file class SampleAddress
{
    [DisplayName("Street Label")]
    public string Street { get; set; } = string.Empty;

    [DisplayName("Zip Code Label")]
    public string ZipCode { get; set; } = string.Empty;
}
 
