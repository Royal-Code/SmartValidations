using System.Numerics;

namespace RoyalCode.SmartValidations.Tests.Predicates;

public partial class BuildInPredicatesTests
{
    [Theory]
    [MemberData(nameof(Numbers_Data))]
    public void Number_NullOrNotEmpty<T>(T value, bool expected)
        where T : INumber<T>
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NullOrNotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Numbers_Data))]
    public void NullableNumber_NullOrNotEmpty<T>(T? value, bool expected)
        where T : struct, INumber<T>
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NullOrNotEmpty(value);

        // Assert
        Assert.Equal(expected, result);

        // Act
        var result2 = BuildInPredicates.NullOrNotEmpty((byte?)null);
        // Assert
        Assert.True(result2);

        // Act
        var result3 = BuildInPredicates.NullOrNotEmpty((short?)null);
        // Assert
        Assert.True(result3);

        // Act
        var result4 = BuildInPredicates.NullOrNotEmpty((long?)null);
        // Assert

        Assert.True(result4);
        // Act
        var result5 = BuildInPredicates.NullOrNotEmpty((float?)null);

        // Assert
        Assert.True(result5);
        // Act
        var result6 = BuildInPredicates.NullOrNotEmpty((double?)null);

        // Assert
        Assert.True(result6);
        // Act
        var result7 = BuildInPredicates.NullOrNotEmpty((decimal?)null);

        // Assert
        Assert.True(result7);
        // Act
        var result8 = BuildInPredicates.NullOrNotEmpty((BigInteger?)null);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("a", true)]
    [InlineData("abc", true)]
    [InlineData(null, true)]
    public void String_NullOrNotEmpty(string? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NullOrNotEmpty(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, null, true)]
    [InlineData(null, "", false)]
    [InlineData("", null, false)]
    [InlineData("", "", false)]
    [InlineData(null, " ", false)]
    [InlineData(" ", null, false)]
    [InlineData(" ", "", false)]
    [InlineData("", " ", false)]
    [InlineData(" ", " ", false)]
    [InlineData("a", null, false)]
    [InlineData("a", "", false)]
    [InlineData("a", " ", false)]
    [InlineData(null, "a", false)]
    [InlineData("", "a", false)]
    [InlineData(" ", "a", false)]
    [InlineData("a", "b", true)]
    public void BothNullOrNotEmpty(string? v1, string? v2, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.BothNullOrNotEmpty(v1, v2);

        // Assert
        Assert.Equal(expected, result);
    }
}