using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using RoyalCode.SmartProblems;

namespace RoyalCode.SmartValidations;

public interface IRuleSet<out TSelf>
    where TSelf: IRuleSet<TSelf>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    TSelf AddProblem(Problem problem);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    string GetDisplayName(string? property);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool HasProblems([NotNullWhen(true)] out Problems? problems);
}

/// <summary>
/// Struct to apply validation rules and collect the result.
/// </summary>
public readonly struct RuleSet : IRuleSet<RuleSet> 
{
    private readonly Type? type;
    private readonly Problems? problems;

    public RuleSet(Type? type = null, Problems? problems = null)
    {
        this.type = type;
        this.problems = problems;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet AddProblem(Problem problem)
    {
        Problems resultProblems = problems ?? new();
        resultProblems.Add(problem);
        return new RuleSet(type, resultProblems);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetDisplayName(string? property)
    {
        return Rules.Resources.DisplayNames.GetDisplayName(type, property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasProblems([NotNullWhen(true)] out Problems? problems)
    {
        problems = this.problems;
        return problems != null;
    }

    #region Not Null

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotNull<TValue>(
        [NotNullWhen(true)] TValue value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value is not null)
            return this;

        return NullOrEmptyProblem(property);
    }

    #endregion

    #region Not Empty

    // [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // public RuleSet NotEmpty(
    //     [NotNullWhen(true)] string? value,
    //     [CallerArgumentExpression(nameof(value))] string? property = null)
    // {
    //     if (!string.IsNullOrWhiteSpace(value))
    //         return this;
    //
    //     return NullOrEmptyProblem(property);
    // }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty<T>(
        [NotNullWhen(true)] T value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
        where T: INumber<T>
    {
        return value != T.Zero ? this : NullOrEmptyProblem(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty<T>(
        [NotNullWhen(true)] T[]? value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value is not null && value.Length > 0)
            return this;

        return NullOrEmptyProblem(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty<T>(
        [NotNullWhen(true)] ICollection<T>? value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value is not null && value.Count > 0)
            return this;

        return NullOrEmptyProblem(property);
    }

    // [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // public RuleSet NotEmpty<T>(
    //     [NotNullWhen(true)] IEnumerable<T>? value,
    //     [CallerArgumentExpression(nameof(value))] string? property = null)
    // {
    //     if (value is not null && value.Any())
    //         return this;
    //
    //     return NullOrEmptyProblem(property);
    // }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty(
        DateTime value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value != DateTime.MinValue && value != DateTime.UnixEpoch)
            return this;

        return NullOrEmptyProblem(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty(
        [NotNullWhen(true)] DateTime? value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value.HasValue && value.Value != DateTime.MinValue && value.Value != DateTime.UnixEpoch)
            return this;
        
        return NullOrEmptyProblem(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty(
        DateTimeOffset value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value != DateTimeOffset.MinValue && value != DateTimeOffset.UnixEpoch)
            return this;

        return NullOrEmptyProblem(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty(
        [NotNullWhen(true)] DateTimeOffset? value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value.HasValue && value.Value != DateTimeOffset.MinValue && value.Value != DateTimeOffset.UnixEpoch)
            return this;

        return NullOrEmptyProblem(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty(
        DateOnly value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value != DateOnly.MinValue && value != new DateOnly(1970, 1, 1))
            return this;

        return NullOrEmptyProblem(property);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEmpty(
        [NotNullWhen(true)] DateOnly? value,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value.HasValue && value.Value != DateOnly.MinValue && value.Value != new DateOnly(1970, 1, 1))
            return this;

        return NullOrEmptyProblem(property);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet BothNullOrNotEmpty(
        [NotNullWhen(true)] string? value1,
        [NotNullWhen(true)] string? value2,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
    {
        if ((value1 is not null || value2 is not null)
            && (string.IsNullOrWhiteSpace(value1) || string.IsNullOrWhiteSpace(value2)))
        {
            var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
            var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);
            AddProblem(Problems.InvalidParameter(
                string.Format(Rules.Resources.BothNullOrNotMessageTemplate, property1Name, property2Name)));
        }

        return this;
    }

    #endregion

    #region Equals Not Equals

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEqual(
        string? value, 
        string expected,
        StringComparison comparison = StringComparison.Ordinal,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        ArgumentNullException.ThrowIfNull(expected);

        if (!expected.Equals(value, comparison))
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.NotEqualMessageTemplate, propertyName, expected), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet NotEqual<T>(
        T? value,
        T expected,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        ArgumentNullException.ThrowIfNull(expected);

        if (!expected.Equals(value))
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.NotEqualMessageTemplate, propertyName, expected), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet Equal(
        string? value,
        string expected,
        StringComparison comparison = StringComparison.Ordinal,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        ArgumentNullException.ThrowIfNull(expected);

        if (expected.Equals(value, comparison))
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.EqualMessageTemplate, propertyName, expected), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet Equal<T>(
        T? value,
        T expected,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        ArgumentNullException.ThrowIfNull(expected);

        if (expected.Equals(value))
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.EqualMessageTemplate, propertyName, expected), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet BothNotEqual(
        string? value1,
        string? value2,
        StringComparison comparison = StringComparison.Ordinal,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
    {
        if (!string.Equals(value1, value2, comparison))
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.BothNotEqualMessageTemplate, property1Name, property2Name)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet BothNotEqual<T>(
        T? value1,
        T? value2,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
    {
        if (!Equals(value1, value2))
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.BothNotEqualMessageTemplate, property1Name, property2Name)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet BothEqual(
        string? value1,
        string? value2,
        StringComparison comparison = StringComparison.Ordinal,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
    {
        if (string.Equals(value1, value2, comparison))
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.BothEqualMessageTemplate, property1Name, property2Name)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet BothEqual<T>(
        T? value1,
        T? value2,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
    {
        if (Equals(value1, value2))
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.BothEqualMessageTemplate, property1Name, property2Name)));
    }

    #endregion

    #region Min Max

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet Min<T>(T value, T min, [CallerArgumentExpression(nameof(value))] string? property = null)
        where T : IComparable<T>
    {
        if (min.CompareTo(value) <= 0)
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.MinMessageTemplate, propertyName, min.ToString()), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet MinLength(string? value, int minLength, [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value is not null && value.Length >= minLength)
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.MinLengthMessageTemplate, propertyName, minLength.ToString()), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet Max<T>(T value, T max, [CallerArgumentExpression(nameof(value))] string? property = null)
        where T : IComparable<T>
    {
        if (max.CompareTo(value) >= 0)
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.MaxMessageTemplate, propertyName, max.ToString()), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet MaxLength(string? value, int maxLength, [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value is null || value.Length <= maxLength)
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.MaxLengthMessageTemplate, propertyName, maxLength.ToString()), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet MinMax<T>(T value, T min, T max, [CallerArgumentExpression(nameof(value))] string? property = null)
        where T : IComparable<T>
    {
        if (min.CompareTo(max) <= 0 && max.CompareTo(value) >= 0)
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.MinMaxMessageTemplate, propertyName, min.ToString(), max.ToString()), property));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet Length(string? value, int minLength, int maxLength, [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (value is not null && value.Length >= minLength && value.Length <= maxLength)
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.LengthMessageTemplate, propertyName, minLength.ToString(), maxLength.ToString()), property));
    }

    #endregion

    #region Less/Greater Than Or Equal

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet LessThan<T>(
        T value1, 
        T value2, 
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
        where T : IComparable<T>
    {
        if (value1.CompareTo(value2) == -1)
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.LessThanMessageTemplate, property1Name, property2Name)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet LessThanOrEqual<T>(
        T value1,
        T value2,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
        where T : IComparable<T>
    {
        if (value1.CompareTo(value2) != 1)
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.LessThanOrEqualMessageTemplate, property1Name, property2Name)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet GreaterThan<T>(
        T value1,
        T value2,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
        where T : IComparable<T>
    {
        if (value1.CompareTo(value2) == 1)
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.GreaterThanMessageTemplate, property1Name, property2Name)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RuleSet GreaterThanOrEqual<T>(
        T value1,
        T value2,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
        where T : IComparable<T>
    {
        if (value1.CompareTo(value2) != -1)
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.GreaterThanOrEqualMessageTemplate, property1Name, property2Name)));
    }

    #endregion

    #region Must

    public RuleSet Must<T>(
        T value, 
        Func<T, bool> predicate, 
        Func<string, T, string> messageFormatter,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (predicate(value)) 
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(messageFormatter(propertyName, value), property));
    }

    public RuleSet Must<TValue, TParam>(
        TValue value,
        TParam param,
        Func<TValue, TParam, bool> predicate,
        Func<string, TValue, TParam, string> messageFormatter,
        [CallerArgumentExpression(nameof(value))] string? property = null)
    {
        if (predicate(value, param))
            return this;

        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(messageFormatter(propertyName, value, param), property));
    }

    public RuleSet BothMust<T1, T2>(
        T1 value1,
        T2 value2,
        Func<T1, T2, bool> predicate,
        Func<string, string, T1, T2, string> messageFormatter,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
    {
        if (predicate(value1, value2))
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(messageFormatter(property1Name, property2Name, value1, value2)));
    }

    public RuleSet BothMust<T1, T2, TParam>(
        T1 value1,
        T2 value2,
        TParam param,
        Func<T1, T2, TParam, bool> predicate,
        Func<string, string, T1, T2, TParam, string> messageFormatter,
        [CallerArgumentExpression(nameof(value1))] string? property1 = null,
        [CallerArgumentExpression(nameof(value2))] string? property2 = null)
    {
        if (predicate(value1, value2, param))
            return this;

        var property1Name = Rules.Resources.DisplayNames.GetDisplayName(type, property1);
        var property2Name = Rules.Resources.DisplayNames.GetDisplayName(type, property2);

        return AddProblem(Problems.InvalidParameter(
            messageFormatter(property1Name, property2Name, value1, value2, param)));
    }

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RuleSet NullOrEmptyProblem(string? property)
    {
        var propertyName = Rules.Resources.DisplayNames.GetDisplayName(type, property);

        return AddProblem(Problems.InvalidParameter(
            string.Format(Rules.Resources.NotNullOrEmptyMessageTemplate, propertyName), property));
    }
}
