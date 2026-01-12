using System.Numerics;

namespace RoyalCode.SmartValidations.Tests.Predicates;

public partial class BuildInPredicatesTests
{
    [Theory]
    [MemberData(nameof(Comparable_Data))]
    public void Comparable_Min<T>(T value, T min, int compare)
        where T : IComparable<T>
    {
        // Arrange
        bool expected = compare >= 0;

        // Act
        var result = BuildInPredicates.Min(value, min);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Comparable_Data))]
    public void Nullable_Comparable_Min<T>(T? value, T min, int compare)
        where T : struct, IComparable<T>
    {
        // Arrange
        bool expected = compare >= 0;

        // Act
        var result = BuildInPredicates.Min(value, min);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, 0, false)]
    [InlineData(null, 1, false)]
    [InlineData(0, 0, true)]
    [InlineData(2, 1, true)]
    [InlineData(1, 2, false)]
    public void Comparable_Nulls_Min(int? value,  int min, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Min(value, min);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Comparable_Data))]
    public void Comparable_Max<T>(T value, T max, int compare)
        where T : IComparable<T>
    {
        // Arrange
        bool expected = compare <= 0;

        // Act
        var result = BuildInPredicates.Max(value, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Comparable_Data))]
    public void Nullable_Comparable_Max<T>(T? value, T max, int compare)
        where T : struct, IComparable<T>
    {
        // Arrange
        bool expected = compare <= 0;

        // Act
        var result = BuildInPredicates.Max(value, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, 0, false)]
    [InlineData(null, 1, false)]
    [InlineData(0, 0, true)]
    [InlineData(2, 1, false)]
    [InlineData(1, 2, true)]
    public void Comparable_Nulls_Max(int? value, int max, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Max(value, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Comparable_MinMax_Data))]
    public void Comparable_MinMax<T>(T value, T min, T max, bool expected)
        where T : IComparable<T>
    {
        // Arrange
        // Act
        var result = BuildInPredicates.MinMax(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Comparable_MinMax_Data))]
    [InlineData(null, 0, 0, false)]
    [InlineData(null, 0, 1, false)]
    [InlineData(null, 1, 3, false)]
    public void Comparable_Nulls_MinMax(int? value, int min, int max, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.MinMax(value, min, max);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, 0, false)]
    [InlineData("", 0, true)]
    [InlineData("a", 0, true)]
    [InlineData("", 1, false)]
    [InlineData("a", 1, true)]
    [InlineData("ab", 1, true)]
    public void String_MinLength(string? value, int minLength, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.MinLength(value, minLength);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, 0, true)]
    [InlineData("", 0, true)]
    [InlineData("a", 0, false)]
    [InlineData("", 1, true)]
    [InlineData("a", 1, true)]
    [InlineData("ab", 1, false)]
    public void String_MaxLength(string? value, int maxLength, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.MaxLength(value, maxLength);
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, 0, 0, false)]
    [InlineData(null, 0, 1, false)]
    [InlineData("", 0, 0, true)]
    [InlineData("", 0, 1, true)]
    [InlineData("a", 0, 0, false)]
    [InlineData("a", 0, 1, true)]
    [InlineData("ab", 0, 1, false)]
    public void String_Length(string? value, int minLength, int maxLength, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Length(value, minLength, maxLength);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> Nullable_Comparable_Data()
    {
        foreach(var data in Comparable_Data())
            yield return data;

        yield return [(byte?)0, (byte)1, -1];
        yield return [(byte?)1, (byte)1, 0];
        yield return [(byte?)2, (byte)1, 1];

        yield return [(short?)0, (short)1, -1];
        yield return [(short?)1, (short)1, 0];
        yield return [(short?)2, (short)1, 1];

        yield return [(int?)0, 1, -1];
        yield return [(int?)1, 1, 0];
        yield return [(int?)2, 1, 2];

        yield return [(long?)0, 1L, -1];
        yield return [(long?)1, 1L, 0];
        yield return [(long?)2, 1L, 1];

        yield return [(float?)0, 1f, -1];
        yield return [(float?)1, 1f, 0];
        yield return [(float?)2, 1f, 1];

        yield return [(double?)0, 1d, -1];
        yield return [(double?)1, 1d, 0];
        yield return [(double?)2, 1d, 1];

        yield return [(decimal?)0, 1M, -1];
        yield return [(decimal?)1, 1M, 0];
        yield return [(decimal?)2, 1M, 1];
    }

    public static IEnumerable<object[]> Comparable_Data()
    {
        yield return [(byte)0, (byte)1, -1];
        yield return [(byte)1, (byte)1, 0];
        yield return [(byte)2, (byte)1, 1];
        
        yield return [(short)0, (short)1, -1];
        yield return [(short)1, (short)1, 0];
        yield return [(short)2, (short)1, 1];
        
        yield return [0, 1, -1];
        yield return [1, 1, 0];
        yield return [2, 1, 1];
        
        yield return [0L, 1L, -1];
        yield return [1L, 1L, 0];
        yield return [2L, 1L, 1];
        
        yield return [0f, 1f, -1];
        yield return [1f, 1f, 0];
        yield return [2f, 1f, 1];
        
        yield return [0d, 1d, -1];
        yield return [1d, 1d, 0];
        yield return [2d, 1d, 1];
        
        yield return [0M, 1M, -1];
        yield return [1M, 1M, 0];
        yield return [2M, 1M, 1];

        yield return [new BigInteger(0), BigInteger.One, -1];
        yield return [BigInteger.One, BigInteger.One, 0];
        yield return [new BigInteger(2), BigInteger.One, 1];
    }

    public static IEnumerable<object[]> Comparable_MinMax_Data()
    {
        yield return [0, 1, 3, false];
        yield return [1, 1, 3, true];
        yield return [2, 1, 3, true];
        yield return [3, 1, 3, true];
        yield return [4, 1, 3, false];
    }
}
