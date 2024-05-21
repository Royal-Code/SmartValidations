using System.Numerics;

namespace RoyalCode.SmartValidations.Tests;

public class BuildInPredicatesTests
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
    [MemberData(nameof(Numbers_Data))]
    public void Number_NotEmpty<T>(T value, bool expected) where T: INumber<T>
    {
        // Arrange
        // Act
        var result = BuildInPredicates.NotEmpty(value);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    public static IEnumerable<object[]> Numbers_Data()
    {
        yield return [ (byte)1, true ];
        yield return [ (byte)0, false ];
        yield return [ (short)1, true ];
        yield return [ (short)0, false ];
        yield return [ 1, true ];
        yield return [ 0, false ];
        yield return [ 1L, true ];
        yield return [ 0L, false ];
        yield return [ 1f, true ];
        yield return [ 0f, false ];
        yield return [ 1d, true ];
        yield return [ 0d, false ];
        yield return [ 1M, true ];
        yield return [ 0M, false ];
        yield return [ BigInteger.One, true ];
        yield return [ BigInteger.Zero, false ];
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
        yield return [null, false];
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
        yield return [null, false];
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
        yield return [new List<string> { "a" }, true];
        yield return [new List<string>(), false];
        yield return [null, false];
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
        yield return [DateTime.UnixEpoch, false];
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
        yield return [DateTime.UnixEpoch, false];
        yield return [null, false];
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
        yield return [DateTimeOffset.UnixEpoch, false];
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
        yield return [DateTimeOffset.UnixEpoch, false];
        yield return [null, false];
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
        yield return [new DateOnly(1970, 1, 1), false];
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
        yield return [new DateOnly(1970, 1, 1), false];
        yield return [null, false];
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
        yield return [null, false];
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