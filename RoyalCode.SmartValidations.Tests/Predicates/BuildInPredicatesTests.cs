namespace RoyalCode.SmartValidations.Tests.Predicates;

public partial class BuildInPredicatesTests
{
    [Theory]
    [InlineData("contact@royal-code.com", true)]
    [InlineData("contact@royal-code", true)]
    [InlineData("contact@", false)]
    [InlineData("@royal-code.com", false)]
    [InlineData("royal-code.com", false)]
    public void IsEmail(string value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.IsEmail(value);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("https://royal-code.com", true)]
    [InlineData("http://royal-code.com/path/1/path", true)]
    [InlineData("http://royal-code.com/path/file.json", true)]
    [InlineData("http://royal-code.com?x=1#id", true)]
    [InlineData("ftp://royal-code.com", true)]
    [InlineData("royal-code.com", false)]
    public void IsUrl(string value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.IsUrl(value);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("https://royal-code.com", true)]
    [InlineData("https://royal-code.com/path/1", true)]
    [InlineData("http://royal-code.com", false)]
    [InlineData("ftp://royal-code.com", false)]
    [InlineData("/orders/1", false)]
    [InlineData(null, false)]
    public void IsHttpsUrl(string? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.IsHttpsUrl(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("https://royal-code.com", true)]
    [InlineData("http://royal-code.com/path/1", true)]
    [InlineData("ftp://royal-code.com", true)]
    [InlineData("/orders/1", false)]
    [InlineData("orders/1", false)]
    [InlineData(null, false)]
    public void IsAbsoluteUrl(string? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.IsAbsoluteUrl(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("/orders/1", true)]
    [InlineData("orders/1", true)]
    [InlineData("../orders/1", true)]
    [InlineData("?page=1", true)]
    [InlineData("https://royal-code.com", false)]
    [InlineData("http://royal-code.com/path/1", false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData(null, false)]
    public void IsRelativeUrl(string? value, bool expected)
    {
        // Arrange
        // Act
        var result = BuildInPredicates.IsRelativeUrl(value);

        // Assert
        Assert.Equal(expected, result);
    }
}
