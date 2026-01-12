namespace RoyalCode.SmartValidations.Tests.Predicates;

public partial class BuildInPredicatesTests
{
    [Theory]
    [MemberData(nameof(Must_Data))]
    public void Must<T>(T value, Func<T, bool> predicate, bool expectProblems)
    {
        // Arrange
        var rules = Rules.Set().Must(value, predicate, (p, v) => $"{v} is not a valid value for {p}");

        // Act
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.Equal(expectProblems, hasProblems);

        if (hasProblems)
            Assert.Single(problems!);
        else
            Assert.Null(problems);
    }

    [Theory]
    [MemberData(nameof(Must_Data_WithParam))]
    public void Must_WithParam<TValue, TParam>(TValue value, TParam param, Func<TValue, TParam, bool> predicate, bool expectProblems)
    {
        // Arrange
        var rules = Rules.Set().Must(value, param, predicate, (p, r, v) => $"{v} is not a valid value for {p} and {r}");

        // Act
        var hasProblems = rules.HasProblems(out var problems);
        // Assert
        Assert.Equal(expectProblems, hasProblems);
        if (hasProblems)
            Assert.Single(problems!);
        else
            Assert.Null(problems);
    }

    [Theory]
    [MemberData(nameof(BothMust_Data))]
    public void BothMust<T1, T2>(T1 value1, T2 value2, Func<T1, T2, bool> predicate, bool expectProblems)
    {
        // Arrange
        var rules = Rules.Set().BothMust(value1, value2, predicate,
            (p1, p2, v1, v2) => $"{v1} and {v2} are not valid values for {p1} and {p2}");

        // Act
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.Equal(expectProblems, hasProblems);

        if (hasProblems)
            Assert.Single(problems!);
        else
            Assert.Null(problems);
    }

    [Theory]
    [MemberData(nameof(BothMust_Data_WithParam))]
    public void BothMust_WithParam<T1, T2, TParam>(T1 value1, T2 value2, TParam param, Func<T1, T2, TParam, bool> predicate, bool expectProblems)
    {
        // Arrange
        var rules = Rules.Set().BothMust(value1, value2, param, predicate, 
            (p1, p2, r, v1, v2) => $"{v1} and {v2} are not valid values for {p1} and {p2} and {r}");

        // Act
        var hasProblems = rules.HasProblems(out var problems);

        // Assert
        Assert.Equal(expectProblems, hasProblems);

        if (hasProblems)
            Assert.Single(problems!);
        else
            Assert.Null(problems);
    }


    public static IEnumerable<object?[]> Must_Data()
    {
        yield return [1, (Func<int, bool>)(x => x > 0), false];
        yield return [1, (Func<int, bool>)(x => x < 0), true];
    }

    public static IEnumerable<object?[]> Must_Data_WithParam()
    {
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x > 0 && x < y), false];
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x < 0 || x > y), true];
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x < 0 || x == y), true];
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x > 0 && x == y), true];
    }

    public static IEnumerable<object?[]> BothMust_Data()
    {
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x > 0 && x < y), false];
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x < 0 || x > y), true];
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x < 0 || x == y), true];
        yield return [1, 2, (Func<int, int, bool>)((x, y) => x > 0 && x == y), true];
    }

    public static IEnumerable<object?[]> BothMust_Data_WithParam()
    {
        yield return [1, 2, 3, (Func<int, int, int, bool>)((x, y, z) => x > 0 && x < y && y < z), false];
        yield return [1, 2, 3, (Func<int, int, int, bool>)((x, y, z) => x < 0 || x > y || y > z), true];
        yield return [1, 2, 3, (Func<int, int, int, bool>)((x, y, z) => x < 0 || x == y || y == z), true];
        yield return [1, 2, 3, (Func<int, int, int, bool>)((x, y, z) => x > 0 && x == y && y == z), true];
    }
}
