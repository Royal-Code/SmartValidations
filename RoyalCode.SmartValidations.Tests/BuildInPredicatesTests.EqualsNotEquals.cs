namespace RoyalCode.SmartValidations.Tests;

public partial class BuildInPredicatesTests
{
    [Theory]
    [InlineData("", "", null, true)]
    [InlineData("abc", "abc", null, true)]
    [InlineData("abc", "abcdfg", null, false)]
    [InlineData("abc", "ABC", null, false)]
    [InlineData("abc", "ABC", StringComparison.OrdinalIgnoreCase, true)]
    [InlineData("abc", "ABCdfg", StringComparison.OrdinalIgnoreCase, false)]
    [InlineData(null, "", null, false)]
    public void String_Equals(string? value, string compare, StringComparison? comparison, bool expected)
    {
        // Arrange
        comparison ??= StringComparison.Ordinal;

        // Act
        var areEquals = BuildInPredicates.Equal(value, compare, comparison.Value);

        // Assert
        Assert.Equal(expected, areEquals);
    }
}
