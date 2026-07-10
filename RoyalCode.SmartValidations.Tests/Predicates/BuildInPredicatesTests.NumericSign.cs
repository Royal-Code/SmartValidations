namespace RoyalCode.SmartValidations.Tests.Predicates;

public partial class BuildInPredicatesTests
{
    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void Numeric_Positive(int value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Positive(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(null, false)]
    public void Nullable_Numeric_Positive(int? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Positive(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(0, false)]
    [InlineData(-1, true)]
    public void Numeric_Negative(int value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Negative(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(0, false)]
    [InlineData(-1, true)]
    [InlineData(null, false)]
    public void Nullable_Numeric_Negative(int? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Negative(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(0, true)]
    [InlineData(-1, false)]
    public void Numeric_Zero(int value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Zero(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(0, true)]
    [InlineData(-1, false)]
    [InlineData(null, false)]
    public void Nullable_Numeric_Zero(int? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.Zero(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(-1, true)]
    public void Numeric_NotZero(int value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotZero(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(-1, true)]
    [InlineData(null, false)]
    public void Nullable_Numeric_NotZero(int? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotZero(value);

        // Assert
        Assert.Equal(expected, result);
    }
}
