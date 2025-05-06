namespace RoyalCode.SmartValidations.Tests;

public partial class BuildInPredicatesTests
{
    [Theory]
    [MemberData(nameof(Comparable_Data))]
    public void Comparable_LessThan<T>(T value, T other, int compare)
        where T : IComparable<T>
    {
        // Arrange
        bool expected = compare < 0;

        // Act
        var result = BuildInPredicates.LessThan(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Comparable_Data))]
    public void Nullable_Comparable_LessThan<T>(T? value, T? other, int compare)
        where T : struct, IComparable<T>
    {
        // Arrange
        bool expected = compare < 0;

        // Act
        var result = BuildInPredicates.LessThan(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(null, 0, -1)]
    [InlineData(0, null, 1)]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, -1)]
    public void Comparable_Nulls_LessThan(int? value, int? other, int compare)
    {
        // Arrange
        bool expected = compare < 0;

        // Act
        var result = BuildInPredicates.LessThan(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Comparable_Data))]
    public void Comparable_LessThanOrEqual<T>(T value, T other, int compare)
        where T : IComparable<T>
    {
        // Arrange
        bool expected = compare <= 0;

        // Act
        var result = BuildInPredicates.LessThanOrEqual(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Comparable_Data))]
    public void Nullable_Comparable_LessThanOrEqual<T>(T? value, T? other, int compare)
        where T : struct, IComparable<T>
    {
        // Arrange
        bool expected = compare <= 0;

        // Act
        var result = BuildInPredicates.LessThanOrEqual(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(null, 0, -1)]
    [InlineData(0, null, 1)]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, -1)]
    public void Comparable_Nulls_LessThanOrEqual(int? value, int? other, int compare)
    {
        // Arrange
        bool expected = compare <= 0;

        // Act
        var result = BuildInPredicates.LessThanOrEqual(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Comparable_Data))]
    public void Comparable_GreaterThan<T>(T value, T other, int compare)
        where T : IComparable<T>
    {
        // Arrange
        bool expected = compare > 0;

        // Act
        var result = BuildInPredicates.GreaterThan(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Comparable_Data))]
    public void Nullable_Comparable_GreaterThan<T>(T? value, T? other, int compare)
        where T : struct, IComparable<T>
    {
        // Arrange
        bool expected = compare > 0;

        // Act
        var result = BuildInPredicates.GreaterThan(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(null, 0, -1)]
    [InlineData(0, null, 1)]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, -1)]
    public void Comparable_Nulls_GreaterThan(int? value, int? other, int compare)
    {
        // Arrange
        bool expected = compare > 0;

        // Act
        var result = BuildInPredicates.GreaterThan(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Comparable_Data))]
    public void Comparable_GreaterThanOrEqual<T>(T value, T other, int compare)
        where T : IComparable<T>
    {
        // Arrange
        bool expected = compare >= 0;

        // Act
        var result = BuildInPredicates.GreaterThanOrEqual(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(Nullable_Comparable_Data))]
    public void Nullable_Comparable_GreaterThanOrEqual<T>(T? value, T? other, int compare)
        where T : struct, IComparable<T>
    {
        // Arrange
        bool expected = compare >= 0;

        // Act
        var result = BuildInPredicates.GreaterThanOrEqual(value, other);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(null, 0, -1)]
    [InlineData(0, null, 1)]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, -1)]
    public void Comparable_Nulls_GreaterThanOrEqual(int? value, int? other, int compare)
    {
        // Arrange
        bool expected = compare >= 0;

        // Act
        var result = BuildInPredicates.GreaterThanOrEqual(value, other);

        // Assert
        Assert.Equal(expected, result);
    }
}
