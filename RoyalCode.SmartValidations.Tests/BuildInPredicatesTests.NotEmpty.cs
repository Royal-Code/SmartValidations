using System.Numerics;

namespace RoyalCode.SmartValidations.Tests;

public partial class BuildInPredicatesTests
{
    [Theory]
    [MemberData(nameof(Numbers_Data))]
    public void Number_NotEmpty<T>(T value, bool expected) where T : INumber<T>
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Numbers_Data))]
    public void NullableNumber_NotEmpty<T>(T? value, bool expected) where T : struct, INumber<T>
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(null, false)]
    public void NullableInt_NotEmpty(int? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);

        // Act
        var result2 = BuildInPredicates.NotEmpty((byte?)null);
        // Assert
        Assert.False(result2);

        // Act
        var result3 = BuildInPredicates.NotEmpty((short?)null);
        // Assert
        Assert.False(result3);

        // Act
        var result4 = BuildInPredicates.NotEmpty((long?)null);
        // Assert
        Assert.False(result4);

        // Act
        var result5 = BuildInPredicates.NotEmpty((float?)null);
        // Assert
        Assert.False(result5);

        // Act
        var result6 = BuildInPredicates.NotEmpty((double?)null);
        // Assert
        Assert.False(result6);

        // Act
        var result7 = BuildInPredicates.NotEmpty((decimal?)null);
        // Assert
        Assert.False(result7);

        // Act
        var result8 = BuildInPredicates.NotEmpty((BigInteger?)null);
        // Assert
        Assert.False(result8);
    }

    public static IEnumerable<object[]> Nullable_Numbers_Data()
    {
        foreach (var value in Numbers_Data())
        {
            yield return value;
        }

        yield return [(byte?)1, true];
        yield return [(byte?)0, false];
        yield return [(short?)1, true];
        yield return [(short?)0, false];
        yield return [(int?)1, true];
        yield return [(int?)0, false];
        yield return [(long?)1L, true];
        yield return [(long?)0L, false];
        yield return [(float?)1f, true];
        yield return [(float?)0f, false];
        yield return [(double?)1d, true];
        yield return [(double?)0d, false];
        yield return [(decimal?)1M, true];
        yield return [(decimal?)0M, false];
        yield return [(BigInteger?)BigInteger.One, true];
        yield return [(BigInteger?)BigInteger.Zero, false];
    }

    public static IEnumerable<object[]> Numbers_Data()
    {
        yield return [(byte)1, true];
        yield return [(byte)0, false];
        yield return [(short)1, true];
        yield return [(short)0, false];
        yield return [1, true];
        yield return [0, false];
        yield return [1L, true];
        yield return [0L, false];
        yield return [1f, true];
        yield return [0f, false];
        yield return [1d, true];
        yield return [0d, false];
        yield return [1M, true];
        yield return [0M, false];
        yield return [BigInteger.One, true];
        yield return [BigInteger.Zero, false];
    }

    [Theory]
    [MemberData(nameof(Arrays_Data))]
    public void Array_NotEmpty<T>(T[]? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> Arrays_Data()
    {
        yield return [new string[1], true];
        yield return [new string[0], false];
        yield return [null!, false];
    }

    [Theory]
    [MemberData(nameof(Collections_Data))]
    public void Collection_NotEmpty<T>(ICollection<T>? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> Collections_Data()
    {
        yield return [new List<string> { "a" }, true];
        yield return [new List<string>(), false];
        yield return [null!, false];
    }

    [Theory]
    [MemberData(nameof(Enumerables_Data))]
    public void Enumerable_NotEmpty<T>(IEnumerable<T>? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> Enumerables_Data()
    {
        yield return [new List<string> { "e" }, true];
        yield return [new List<string>(), false];
        yield return [null!, false];
    }

    [Theory]
    [MemberData(nameof(DateTime_Data))]
    public void DateTime_NotEmpty(DateTime value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DateTime_Data()
    {
        yield return [DateTime.Now, true];
        yield return [DateTime.MinValue, false];
        yield return [DateTime.UnixEpoch, true];
    }

    [Theory]
    [MemberData(nameof(DateTimeNullable_Data))]
    public void DateTimeNullable_NotEmpty(DateTime? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DateTimeNullable_Data()
    {
        yield return [DateTime.Now, true];
        yield return [DateTime.MinValue, false];
        yield return [DateTime.UnixEpoch, true];
        yield return [null!, false];
    }

    [Theory]
    [MemberData(nameof(DateTimeOffset_Data))]
    public void DateTimeOffset_NotEmpty(DateTimeOffset value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DateTimeOffset_Data()
    {
        yield return [DateTimeOffset.Now, true];
        yield return [DateTimeOffset.MinValue, false];
        yield return [DateTimeOffset.UnixEpoch, true];
    }

    [Theory]
    [MemberData(nameof(DateTimeOffsetNullable_Data))]
    public void DateTimeOffsetNullable_NotEmpty(DateTimeOffset? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DateTimeOffsetNullable_Data()
    {
        yield return [DateTimeOffset.Now, true];
        yield return [DateTimeOffset.MinValue, false];
        yield return [DateTimeOffset.UnixEpoch, true];
        yield return [null!, false];
    }

    [Theory]
    [MemberData(nameof(DateOnly_Data))]
    public void DateOnly_NotEmpty(DateOnly value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DateOnly_Data()
    {
        yield return [DateOnly.FromDateTime(DateTime.Now), true];
        yield return [DateOnly.MinValue, false];
        yield return [new DateOnly(1970, 1, 1), true];
    }

    [Theory]
    [MemberData(nameof(DateOnlyNullable_Data))]
    public void DateOnlyNullable_NotEmpty(DateOnly? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> DateOnlyNullable_Data()
    {
        yield return [DateOnly.FromDateTime(DateTime.Now), true];
        yield return [DateOnly.MinValue, false];
        yield return [new DateOnly(1970, 1, 1), true];
        yield return [null!, false];
    }

    [Theory]
    [MemberData(nameof(Guid_Data))]
    public void Guid_NotEmpty(Guid value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> Guid_Data()
    {
        yield return [Guid.NewGuid(), true];
        yield return [Guid.Empty, false];
    }

    [Theory]
    [MemberData(nameof(GuidNullable_Data))]
    public void GuidNullable_NotEmpty(Guid? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GuidNullable_Data()
    {
        yield return [Guid.NewGuid(), true];
        yield return [Guid.Empty, false];
        yield return [null!, false];
    }

    [Theory]
    [InlineData("a", true)]
    [InlineData(" a ", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    public void String_NotEmpty(string? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }
}
