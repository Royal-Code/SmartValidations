namespace RoyalCode.SmartValidations.Tests.RuleSetRules;

public partial class RuleSetTests
{
    [Fact]
    public void When_ConditionTrue_AppliesRules()
    {
        // Arrange
        string name = "";

        // Act
        var set = Rules.Set().When(true, s => s.NotEmpty(name));

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(name), p.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void When_ConditionFalse_DoesNotApplyRules()
    {
        // Arrange
        string name = "";

        // Act
        var set = Rules.Set().When(false, s => s.NotEmpty(name));

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Unless_BoolTrue_SkipsRules()
    {
        // Arrange
        string code = "";

        // Act
        var set = Rules.Set().Unless(true, s => s.NotEmpty(code));

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Unless_BoolFalse_AppliesRules()
    {
        // Arrange
        string code = "";

        // Act
        var set = Rules.Set().Unless(false, s => s.NotEmpty(code));

        // Assert
        Assert.True(set.HasProblems(out var problems));
        var p = Assert.Single(problems!);
        Assert.Equal(nameof(code), p.Property);
        Assert.Equal(Rules.NotNullOrNotEmpty, p.Extensions![Rules.RuleProperty]);
    }

    [Fact]
    public void Unless_Builder_BothGroupsFail_AddsBothProblems()
    {
        // Arrange
        string a = "";
        string b = "";

        // Act
        var set = Rules.Set()
            .Unless(
                s => s.NotEmpty(a),
                s => s.NotEmpty(b));

        // Assert
        Assert.True(set.HasProblems(out var problems));
        Assert.Equal(2, problems!.Count);
    }

    [Fact]
    public void Unless_Builder_ConditionPasses_NoProblemsAdded()
    {
        // Arrange
        string a = "ok";
        string b = "";

        // Act
        var set = Rules.Set()
            .Unless(
                s => s.NotEmpty(a),
                s => s.NotEmpty(b));

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Unless_Builder_AlternativePasses_NoProblemsAdded()
    {
        // Arrange
        string a = "";
        string b = "ok";

        // Act
        var set = Rules.Set()
            .Unless(
                s => s.NotEmpty(a),
                s => s.NotEmpty(b));

        // Assert
        Assert.False(set.HasProblems(out var problems));
        Assert.Null(problems);
    }

    [Fact]
    public void Unless_FactoryAndBuilder_PrefixIsPreservedAndRemovedFromProperty()
    {
        // Arrange
        var model = new { Name = "" };
        var set = Rules.Set().WithPropertyPrefix("model");

        // Act
        set = set.Unless(
            () => Rules.Set().WithPropertyPrefix("model").NotEmpty(model.Name),
            s => s.NotEmpty(model.Name));

        // Assert
        Assert.True(set.HasProblems(out var problems));
        Assert.Equal(2, problems.Count);
        Assert.All(problems, p => Assert.Equal("Name", p.Property));

        // Arrange
        set = Rules.Set().WithPropertyPrefix("model");

        // Act
        set = set.Unless(
            s => s.NotEmpty(model.Name),
            s => s.NotEmpty(model.Name));

        // Assert
        Assert.True(set.HasProblems(out problems));
        Assert.Equal(2, problems.Count);
        Assert.All(problems, p => Assert.Equal("Name", p.Property));
    }

    [Fact]
    public void Unless_PreEvaluated_AddsWhenBothHaveProblems()
    {
        // Arrange
        string v1 = "";
        string v2 = "";
        var cond = Rules.Set().NotEmpty(v1);
        var alt = Rules.Set().NotEmpty(v2);

        // Act
        var set = Rules.Set().Unless(cond, alt);

        // Assert
        Assert.True(set.HasProblems(out var problems));
        Assert.Equal(2, problems!.Count);
    }
}
