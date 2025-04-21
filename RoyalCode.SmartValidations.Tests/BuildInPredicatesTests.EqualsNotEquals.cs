using System.Numerics;

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

    [Theory]
    [MemberData(nameof(Equitable_Data))]
    public void Equitable_Equals<T>(T value, T compare, bool expected)
        where T : IEquatable<T>
    {
        // Arrange
        // Act
        var areEquals = BuildInPredicates.Equal(value, compare);

        // Assert
        Assert.Equal(expected, areEquals);
    }

    [Theory]
    [InlineData("", "", null, false)]
    [InlineData("abc", "abc", null, false)]
    [InlineData("abc", "abcdfg", null, true)]
    [InlineData("abc", "ABC", null, true)]
    [InlineData("abc", "ABC", StringComparison.OrdinalIgnoreCase, false)]
    [InlineData("abc", "ABCdfg", StringComparison.OrdinalIgnoreCase, true)]
    [InlineData(null, "", null, true)]
    public void String_NotEquals(string? value, string compare, StringComparison? comparison, bool expected)
    {
        // Arrange
        comparison ??= StringComparison.Ordinal;

        // Act
        var areEquals = BuildInPredicates.NotEqual(value, compare, comparison.Value);

        // Assert
        Assert.Equal(expected, areEquals);
    }

    public static IEnumerable<object[]> Equitable_Data()
    {
        yield return [(byte)1, (byte)1, true];
        yield return [(byte)1, (byte)2, false];
        yield return [(short)1, (short)1, true];
        yield return [(short)1, (short)2, false];
        yield return [1, 1, true];
        yield return [1, 2, false];
        yield return [1L, 1L, true];
        yield return [1L, 2L, false];
        yield return [1f, 1f, true];
        yield return [1f, 2f, false];
        yield return [1d, 1d, true];
        yield return [1d, 2d, false];
        yield return [1M, 1M, true];
        yield return [1M, 2M, false];
        yield return [BigInteger.One, BigInteger.One, true];
        yield return [BigInteger.One, BigInteger.Zero, false];
    }
}
