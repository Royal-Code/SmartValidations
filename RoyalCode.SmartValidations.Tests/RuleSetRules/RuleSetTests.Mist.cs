namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void Email_Invalid_ProducesProblem()
    {
        // Arrange
        string? email = "invalid";

        // Act
        var set = Rules.Set().Email(email);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(email), p.Property);
        Assert.Equal(Rules.Email, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(email, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Email_Valid_NoProblems()
    {
        // Arrange
        string? email = "user@example.com";

        // Act
        var set = Rules.Set().Email(email);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Email_Null_ProducesProblem()
    {
        // Arrange
        string? email = null;

        // Act
        var set = Rules.Set().Email(email);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(email), p.Property);
        Assert.Equal(Rules.Email, p.Extensions![Rules.RuleProperty]);
        Assert.Null(p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Url_Invalid_ProducesProblem()
    {
        // Arrange
        string? url = "not-a-url";

        // Act
        var set = Rules.Set().Url(url);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(url), p.Property);
        Assert.Equal(Rules.Url, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(url, p.Extensions[Rules.CurrentValueProperty]);
    }

    [Fact]
    public void Url_Valid_NoProblems()
    {
        // Arrange
        string? url = "https://example.com/path?q=1";

        // Act
        var set = Rules.Set().Url(url);

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Url_WithPropertyPrefix_RemovesPrefixFromProperty()
    {
        // Arrange
        var model = new { Url = "invalid" };

        // Act
        var set = Rules.Set<object>()
            .WithPropertyPrefix("model")
            .Url(model.Url);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal("Url", p.Property);
        Assert.Equal(Rules.Url, p.Extensions![Rules.RuleProperty]);
        Assert.Equal(model.Url, p.Extensions[Rules.CurrentValueProperty]);
    }
}
