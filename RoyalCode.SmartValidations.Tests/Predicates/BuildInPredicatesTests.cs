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
}